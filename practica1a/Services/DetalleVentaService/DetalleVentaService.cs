using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services.VentaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.DetalleVentaService
{
    public class DetalleVentaService : IDetalleVentaService
    {
        private readonly practicaDbContext _db;
        private readonly IVentaService _service;

        public DetalleVentaService(practicaDbContext db, IVentaService service)
        {
            _db = db;
            _service = service;
        }

        public async Task<IEnumerable<DetalleVenta>> GetDetalleVentas()
        {
            return await _db.DetalleVentas.ToListAsync();
        }

        public async Task<ResponseAction<List<DetalleVenta>>> GetDetalleVenta(int id)
        {

            var res = new ResponseAction<List<DetalleVenta>>();
            //var detalleventa = await _db.DetalleVentas.FindAsync(id);

            var deta = await _db.DetalleVentas
              .Where(d => d.Id == id)
              .Include(v => v.Ventas)
              .ToListAsync();

            if (deta == null)
            {
                res.Message = "No existe una DetalleVenta con ese id";
                return res;
            }

            res.Data = deta;
            res.Success = true;
            return res;


        }


        public async Task<ResponseAction<DetalleVenta>> PostDetalleVenta(DetalleVentaDto detalleVentaDto)
        {
            var res = new ResponseAction<DetalleVenta>();
            var usuario = await _db.Usuarios.FindAsync(detalleVentaDto.UserId);

            var fecha = DateTime.Parse(detalleVentaDto.Fecha);

            int resultado = DateTime.Compare(fecha, DateTime.Now);

            if (usuario == null) { res.Message = "No existe el usaurio"; return res; }

            if (resultado > 0) { res.Message = "La fecha no puede ser posterior a la actual"; return res; }

            var detalle = new DetalleVenta
            {
                UsuarioId = usuario.Id,
                Fecha = detalleVentaDto.Fecha,
            };

            _db.DetalleVentas.Add(detalle);
            await _db.SaveChangesAsync();

            res.Message = "bien";
            res.Success = true;
            return res;
        }

        public async Task<ResponseAction<DetalleVenta>> PutDetalleVenta(int id, DetalleVentaDto detalleVentaDto)
        {
            var res = new ResponseAction<DetalleVenta>();
            var iDetalle = await _db.DetalleVentas.FindAsync(id);
            DateTime fecha = DateTime.Parse(detalleVentaDto.Fecha);
            //var detalle = new DetalleVenta();
            var usuario = await _db.Usuarios.FindAsync(detalleVentaDto.UserId);


            if (iDetalle == null) { res.Message = "No existe el id"; return res; }
            if (usuario == null) { res.Message = "No existe el usaurio"; return res; }

            iDetalle.UsuarioId = detalleVentaDto.UserId;
            iDetalle.Fecha = detalleVentaDto.Fecha;
            await _db.SaveChangesAsync();

            res.Message = "Detallede venta actualizada";
            return res;
        }

        public async Task<ResponseAction<DetalleVenta>> DeleteDetalleVenta(int id)
        {
            var res = new ResponseAction<DetalleVenta>();


            var ventasElimnadas = 0;
            var ventas = await _db.Ventas.Where(v => v.DetalleVentaId == id).ToListAsync();

            var detalleId = await _db.DetalleVentas.FindAsync(id);


            if (detalleId == null) { res.Message = "no existe un detalle venta con ese id"; return res; }

            foreach (var venta in ventas)
            {

                await _service.DeleteVenta(venta.Id);
                ventasElimnadas++;

            }

            _db.DetalleVentas.Remove(detalleId);
            await _db.SaveChangesAsync();

            if (ventas.Count > 0)
            {

                res.Message = $"se han eliminado con exito {ventasElimnadas}";
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
