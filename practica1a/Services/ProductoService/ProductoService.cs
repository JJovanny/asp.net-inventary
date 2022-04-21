using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace practica1a.Services.ProductoService
{
    public class ProductoService : IProductoService
    {

        private readonly practicaDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProductoService(practicaDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task<IEnumerable<Producto>> GetProductos()
        {

            return await _db.Productos
                .Include(x => x.Categorias)
                .Where(x => x.Status == 1 || x.Status == null)
                .ToListAsync();

        }

        public async Task<ResponseAction<Producto>> GetProducto(int id)
        {
            var res = new ResponseAction<Producto>();

            var IProducto = await _db.Productos.FindAsync(id);

            if (IProducto == null)
            {
                res.Message = $"El producto con el id {id} no existe";
                res.Data = null;
                res.Success = false;
                return res;

            }

            res.Data = IProducto;
            res.Success = true;
            return res;

        }

        public async Task<ResponseAction<Producto>> PostProducto(ProductoDto productoDto)
        {
            var res = new ResponseAction<Producto>();
            var producto = new Producto();

            if (string.IsNullOrEmpty(productoDto.Nombre) || productoDto.Precio == 0 || productoDto.Stock == 0 || productoDto.CategoriaId == 0 || productoDto.Foto == null)
            {
                res.Message = "Disculpe todos los campos son requeridos";
                return res;
            }
            else
            {
                var categoriaId = await _db.Categorias.FindAsync(productoDto.CategoriaId);

                if (categoriaId == null)
                {
                    res.Message = $"Disculpe la categoria {categoriaId} no existe";
                    return res;

                }

                producto.Nombre = productoDto.Nombre;
                producto.Precio = productoDto.Precio;
                producto.Stock = productoDto.Stock;
                producto.Foto = productoDto.Foto;
                producto.CategoriaId = productoDto.CategoriaId;
                producto.Status = 1;
                _db.Productos.Add(producto);
                await _db.SaveChangesAsync();


                res.Message = "Producto creado con exito";
                res.Success = true;
                return res;
            }



        }


        public async Task<ResponseAction<Producto>> PutProducto(int id, ProductoDto productoDto)
        {
            var res = new ResponseAction<Producto>();
            var Iproducto = await _db.Productos.FindAsync(id);

            if (Iproducto == null)
            {
                res.Message = $"No existe un producto con el id {id}";
                res.Data = null;
                return res;
            }

            Iproducto.Nombre = productoDto.Nombre;
            Iproducto.Precio = productoDto.Precio;
            Iproducto.Stock = productoDto.Stock;
            Iproducto.CategoriaId = productoDto.CategoriaId;
            Iproducto.Status = 1;
            await _db.SaveChangesAsync();

            res.Message = "Producto actualizado";
            res.Success = true;
            return res;

        }


        public async Task<ResponseAction<Producto>> DeleteProducto(int id)
        {
            var res = new ResponseAction<Producto>();

            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var historialProductoEliminado = new HistorialProductosEliminados();
            var userClaims = identity.Claims;
            int.TryParse(userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value, out var idusuario);
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(x => x.Id == idusuario);
            DateTime dateTime = DateTime.Now;


            var IProducto = await _db.Productos.FindAsync(id);

            if (IProducto == null)
            {
                res.Message = $"El producto con el id {id} no existe";
                res.Data = null;
                return res;

            }

            IProducto.Status = 0;

            historialProductoEliminado.UsuarioId = usuario.Id;
            historialProductoEliminado.NombreUsuario = usuario.Nombre;
            historialProductoEliminado.IdProductoEliminado = IProducto.Id;
            historialProductoEliminado.NombreProductoEliminado = IProducto.Nombre;
            historialProductoEliminado.Fecha = dateTime.ToString("yyyy/MM/dd hh:mm:ss");

            _db.HistorialProductosEliminados.Add(historialProductoEliminado);
            await _db.SaveChangesAsync();

            res.Data = IProducto;
            res.Success = true;
            res.Message = "producto elimnado";
            return res;


        }



        public async Task<ResponseAction<List<Producto>>> GetProductoNombre(string producto)
        {
            var res = new ResponseAction<List<Producto>>();

            var productos = await _db.Productos.
                Where(x => x.Nombre.Contains(producto))
                .Include(c => c.Categorias)
                .ToListAsync();

            if (productos.Count == 0) { res.Message = $"Disculpe, el producto con el nombre o iniciales {producto} no existe"; return res; }

            res.Data = productos;
            return res;

        }








    }
}
