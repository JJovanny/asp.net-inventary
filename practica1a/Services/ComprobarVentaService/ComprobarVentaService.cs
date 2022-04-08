using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.ComprobarVentaService
{
    public class ComprobarVentaService : ControllerBase, IComprobarVentaService
    {
        private readonly practicaDbContext _db;

        public ComprobarVentaService(practicaDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> ComprobarOrdenCompra(int id, string verificar)
        {

            var detalleCompraId = await _db.DetalleCompras.FindAsync(id);

            if (detalleCompraId == null) { return NotFound("no existe una detalle de comrpa con ese id"); }

            if (verificar == "verificada")
            {
                detalleCompraId.Verificada = verificar;
                await _db.SaveChangesAsync();

            }
            else if (verificar == "cancelada")
            {

                detalleCompraId.Verificada = verificar;

                //var delleCompraId = await _db.DetalleCompras.FirstOrDefaultAsync(d => d.Id == ordenCompraId.DetalleCompraId);

                var compras = await _db.Compras.Where(c => c.DetalleCompraId == detalleCompraId.Id).ToListAsync();

                foreach (var compra in compras)
                {

                    var productos = await _db.Productos.Where(p => p.Id == compra.ProductoId).ToListAsync();

                    foreach (var producto in productos)
                    {

                        producto.Stock += compra.Cantidad;
                        await _db.SaveChangesAsync();

                    }

                }

            }
            else
            {

                return NotFound("Disculpe,las opciones que tiene son 'verificada' o 'cancelada' ");

            }

            return Ok();


        }





    }
}
