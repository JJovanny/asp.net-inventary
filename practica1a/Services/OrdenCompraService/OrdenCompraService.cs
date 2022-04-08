using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace practica1a.Services.OrdenCompraService
{
    public class OrdenCompraService : ControllerBase, IOrdenCompraService
    {
        private readonly practicaDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

    public OrdenCompraService(practicaDbContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;

    }

    public async Task<IEnumerable<OrdenCompra>> GetOdenCompras()
        {

            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            var userClaims = identity.Claims;

            if (userClaims.FirstOrDefault(r => r.Type == ClaimTypes.Role)?.Value == "Admin")
            {


                return await _db.OrdenCompras.ToListAsync();
            }
            else
            {

                int.TryParse(userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value, out var idusuario);

                return await _db.OrdenCompras.Where(u => u.UsuarioId == idusuario).ToListAsync();

            }


        }


        public async Task<ResponseAction<OrdenCompra>> GetOdenCompra(int id)
        {
            var res = new ResponseAction<OrdenCompra>();
            var ordencompra = await _db.OrdenCompras.FindAsync(id);

            if (ordencompra == null)
            {
                res.Message = $"No existe una orden de compra con el id {id}";
                return res;
            }

            res.Data = ordencompra;
            res.Success = true;
            return res;


        }

        public async Task<ResponseAction<OrdenCompra>> PostOdenCompra(int detalleCompra, IFormFile file)
        {

            var res = new ResponseAction<OrdenCompra>();
            var ordenCompra = new OrdenCompra();
            var DetalleCompraId = await _db.DetalleCompras.FindAsync(detalleCompra);


            if (DetalleCompraId == null) { res.Message = $"Disculpe no existe un detalle compra con el id {DetalleCompraId}"; return res; }

            if (file == null) { res.Message = $"Disculpe es necesario el comprobante de pago"; return res; }

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);

                using (var mss = new MemoryStream(fileBytes))
                {
                    var fileByte = mss.ToArray();
                    string ss = Convert.ToBase64String(fileBytes);

                    ordenCompra.DetalleCompraId = detalleCompra;
                    ordenCompra.Archivo = ss;
                    _db.OrdenCompras.Add(ordenCompra);
                    await _db.SaveChangesAsync();

                }


            }


            res.Message = "Se ha enviado el orden de compra exitosamente";
            res.Success = true;
            return res;
        }


    }
}
