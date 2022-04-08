using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services.CompraService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace practica1a.Services.DetalleCompraService
{
    public class DetalleCompraService : ControllerBase, IDetalleCompraService
    {
        private readonly practicaDbContext _db;
        private readonly ICompraService _compraService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetalleCompraService(practicaDbContext db, ICompraService compraService, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _compraService = compraService;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task<IEnumerable<DetalleCompra>> GetDetalleCompras()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            var userClaims = identity.Claims;

            if (userClaims.FirstOrDefault(r => r.Type == ClaimTypes.Role)?.Value == "Admin")
            {

                return await _db.DetalleCompras
                    .Include(u => u.Usuarios)
                    .ToListAsync();

            }
            else
            {

                int.TryParse(userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value, out var idusuario);

                return await _db.DetalleCompras.Where(u => u.UsuarioId == idusuario).ToListAsync();

            }

        }


        public async Task<ResponseAction<DetalleCompra>> GetDetalleCompra(int id)
        {

            var res = new ResponseAction<DetalleCompra>();

            var deta = await _db.DetalleCompras
              .Where(d => d.Id == id)
              .Include(v => v.Compras)
              .FirstOrDefaultAsync();

            if (deta == null)
            {
                res.Message = "No existe una DetalleCompra con ese id";
                return res;
            }

            res.Data = deta;
            res.Success = true;
            return res;
        }


        public async Task<ResponseAction<DetalleCompra>> PostDetalleCompra(Dto fecha)
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var res = new ResponseAction<DetalleCompra>();


            if (identity != null)
            {

               DateTime convertirFecha = DateTime.Parse(fecha.Dt1);

                int resultado = DateTime.Compare(convertirFecha,DateTime.Now);

                if (resultado > 0) { res.Message = "La fecha no puede ser posterior a la actual"; return res; }

                var userClaims = identity.Claims;


                if (int.TryParse(userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value, out var id))
                {

                    var detalleCompra = new DetalleCompra
                    {
                        UsuarioId = id,
                        Fecha = fecha.Dt1,
                    };

                    _db.DetalleCompras.Add(detalleCompra);
                    await _db.SaveChangesAsync();

                    res.Message = "bien";
                    res.Success = true;
                    res.Data = detalleCompra;
                    return res;
                }
                else
                {
                    res.Message = "No tienes acceso";
                    return res;

                }
            }
            else
            {
                res.Message = "errror";
                return res;
            }

        }


        public async Task<ResponseAction<DetalleCompra>> EnviarComprobanteDePago(DetalleCompraDto deta)
        {

            var res = new ResponseAction<DetalleCompra>();
            var iDetalleCompra = await _db.DetalleCompras.FindAsync(deta.id);

            
            if (iDetalleCompra == null) { res.Message = $"No existe el id {deta.id} dedicha compra"; return res; }

            if (deta.file == null) { res.Message = $"Disculpe es necesario el comprobante de pago"; return res; }

                   
                    iDetalleCompra.Archivo = deta.file;
                    await _db.SaveChangesAsync();


            res.Message = "Comprobante de pago enviado";
            res.Success = true;
            res.Data = iDetalleCompra;
            return res;

        }


        public async Task<ResponseAction<DetalleCompra>> DeleteDetalleCompra(int id)
        {

            var res = new ResponseAction<DetalleCompra>();

            var comprasElimnadas = 0;
            var compras = await _db.Compras.Where(v => v.DetalleCompraId == id).ToListAsync();

            var detalleId = await _db.DetalleCompras.FindAsync(id);


            if (detalleId == null) { res.Message = "no existe un detalle venta con ese id"; return res; }

            foreach (var compra in compras)
            {

                await _compraService.DeleteCompra(compra.Id);
                comprasElimnadas++;

            }

            _db.DetalleCompras.Remove(detalleId);
            await _db.SaveChangesAsync();

            if (compras.Count > 0)
            {

                res.Message = $"se han eliminado con exito {comprasElimnadas}";
                return res;


            }
            else
            {
                res.Message = "404";
                return res;
            }



        }




    }
}
