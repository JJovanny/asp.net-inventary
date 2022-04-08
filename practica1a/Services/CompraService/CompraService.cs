using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.CompraService
{
    public class CompraService : ICompraService
    {
        private readonly practicaDbContext _db;

        public CompraService(practicaDbContext db)
        {
            _db = db;
        }


        public async Task<IEnumerable<Compra>> GetCompras()
        {

            return await _db.Compras.ToListAsync();

        }

        public async Task<ResponseAction<Compra>> GetCompra(int id)
        {
            var res = new ResponseAction<Compra>();
            var compra = await _db.Compras.FindAsync(id);

            if (compra == null)
            {
                res.Message = "No existe una compra con ese id";
                return res;
            }

            res.Data = compra;
            res.Success = true;
            return res;

        }


        public async Task<ResponseAction<Compra>> PostCompra(CompraDto compraDto)
        {

            var res = new ResponseAction<Compra>();

            var producto = await _db.Productos.FindAsync(compraDto.ProductoId);
            var detalleCompra = await _db.DetalleCompras.FindAsync(compraDto.DetalleCompraId);

            if (producto == null) { res.Message = $"Disculpe no existe un producto con el id {producto.Id}"; return res; }
            if (detalleCompra == null) { res.Message = "Disculpe no existe un id para detalle compra"; return res; }
            if (compraDto.Cantidad > producto.Stock) { res.Message = "Disculpe no contamos esa cantidad de productos para la venta"; return res; }
            if (producto.Stock <= 1) { res.Message = "Disculpe no hay el producto en este momento"; return res; }


            var total = producto.Precio * compraDto.Cantidad;
            var subtotal = total;

            var compra = new Compra
            {
                DetalleCompraId = detalleCompra.Id,
                ProductoId = producto.Id,
                Precio = producto.Precio,
                Cantidad = compraDto.Cantidad,
                Total = total,
            };


            detalleCompra.Subtotal = subtotal += detalleCompra.Subtotal;
            producto.Stock -= compraDto.Cantidad;
            _db.Compras.Add(compra);
            await _db.SaveChangesAsync();


            res.Message = "Compra creada exitosamente";
            res.Success = true;
            res.Data = compra;
            return res;
        }




        public async Task<ResponseAction<Compra>> PutCompra(int id, CompraDto compraDto)
        {

            var res = new ResponseAction<Compra>();
            var Icompra = await _db.Compras.FindAsync(id);
            var producto = await _db.Productos.FindAsync(compraDto.ProductoId);
            var detalleCompra = await _db.DetalleCompras.FindAsync(Icompra.DetalleCompraId);


            if (Icompra == null) { res.Message = "Disculpe no existe la compra requerida"; return res; }
            if (producto == null) { res.Message = "Disculpe no existe ese producto"; return res; }
            if (detalleCompra == null) { res.Message = $"Disculpe no existe el id {Icompra.DetalleCompraId} del detalle compra"; return res; }
            if (compraDto.Cantidad > producto.Stock) { res.Message = "Disculpe en este momento no contamos con esa cantidad de productos a la venta"; return res; }

            var total = producto.Precio * compraDto.Cantidad;

            Icompra.DetalleCompraId = detalleCompra.Id;
            Icompra.ProductoId = producto.Id;
            Icompra.Precio = producto.Precio;
            Icompra.Cantidad = compraDto.Cantidad;
            Icompra.Total = total;

            await _db.SaveChangesAsync();

            res.Message = "compra actualizada exitosamente";
            return res;


        }


        public async Task<ResponseAction<Compra>> DeleteCompra(int id)
        {

            var res = new ResponseAction<Compra>();
            var compra = await _db.Compras.FindAsync(id);

            var detalleCompra = await _db.DetalleCompras.FindAsync(compra.DetalleCompraId);

            if (compra == null)
            {
                res.Message = $"No existe una venta con el id {id}";
                return res;
            }

            var Iproducto = await _db.Productos.FindAsync(compra.ProductoId);

            Iproducto.Stock += compra.Cantidad;
            detalleCompra.Subtotal -= compra.Total;

            _db.Compras.Remove(compra);
            await _db.SaveChangesAsync();

            res.Message = "Compra eliminada con exito";
            res.Success = true;
            res.Data = compra;
            return res;
        }





    }
}
