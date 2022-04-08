using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using practica1a.Data;
using Microsoft.EntityFrameworkCore;
using practica1a.DataObj;
using practica1a.Models;

namespace practica1a.Services.VentaService
{
    public class VentaService : IVentaService
    {
        private readonly practicaDbContext _db;
        public VentaService(practicaDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Venta>> GetVentas()
        {
            return await _db.Ventas.ToListAsync();
        }

        public async Task<ResponseAction<Venta>> GetVenta(int id)
        {
            var res = new ResponseAction<Venta>();
            var venta = await _db.Ventas.FindAsync(id);

            if (venta == null)
            {
                res.Message = "No existe una venta con ese id";
                return res;
            }

            res.Data = venta;
            res.Success = true;
            return res;

        }


        public async Task<ResponseAction<Venta>> PostVenta(VentaDto ventaDto)
        {
            var res = new ResponseAction<Venta>();
            var producto = await _db.Productos.FindAsync(ventaDto.ProductoId);
            var detalleVenta = await _db.DetalleVentas.FindAsync(ventaDto.DetalleVentaId);

            if (producto == null) { res.Message = "Disculpe no existe ese producto"; return res; }
            if (detalleVenta == null) { res.Message = "Disculpe no existe un id para detalle venta"; return res; }
            if (ventaDto.Cantidad > producto.Stock) { res.Message = "Disculpe no tiene esa cantidad de productos para la venta"; return res; }
            if (producto.Stock <= 1) { res.Message = "Disculpe ha llegado al limite de stock"; return res; }


            var total = producto.Precio * ventaDto.Cantidad;
            var subtotal = total;

            var venta = new Venta
            {
                DetalleVentaId = detalleVenta.Id,
                ProductoId = producto.Id,
                Precio = producto.Precio,
                Cantidad = ventaDto.Cantidad,
                Total = total,
            };


            detalleVenta.Subtotal = subtotal += detalleVenta.Subtotal;
            producto.Stock -= ventaDto.Cantidad;
            _db.Ventas.Add(venta);
            await _db.SaveChangesAsync();


            res.Message = "Venta creada exitosamente";
            res.Success = true;
            return res;

        }


        public async Task<ResponseAction<Venta>> PutVenta(int id, VentaDto ventaDto)
        {
            var res = new ResponseAction<Venta>();
            var Iventa = await _db.Ventas.FindAsync(id);
            var producto = await _db.Productos.FindAsync(ventaDto.ProductoId);
            var detalleVenta = await _db.DetalleVentas.FindAsync(Iventa.DetalleVentaId);


            if (Iventa == null) { res.Message = "Disculpe no existe la venta requerida"; return res; }
            if (producto == null) { res.Message = "Disculpe no existe ese producto"; return res; }
            if (detalleVenta == null) { res.Message = "Disculpe no existe un id para detalle venta"; return res; }
            if (ventaDto.Cantidad > producto.Stock) { res.Message = "Disculpe no cuenta con el stock necesario para esa venta"; return res; }

            var total = producto.Precio * ventaDto.Cantidad;

            Iventa.DetalleVentaId = detalleVenta.Id;
            Iventa.ProductoId = producto.Id;
            Iventa.Precio = producto.Precio;
            Iventa.Cantidad = ventaDto.Cantidad;
            Iventa.Total = total;

            await _db.SaveChangesAsync();

            res.Message = "venta actualizada exitosamente";
            return res;


        }


        public async Task<ResponseAction<Venta>> DeleteVenta(int id)
        {
            var res = new ResponseAction<Venta>();
            var venta = await _db.Ventas.FindAsync(id);
            var detalleventa = await _db.DetalleVentas.FindAsync(venta.DetalleVentaId);

            if (venta == null)
            {
                res.Message = "No existe una venta con ese id";
                return res;
            }

            var Iproducto = await _db.Productos.FindAsync(venta.ProductoId);

            Iproducto.Stock += venta.Cantidad;
            detalleventa.Subtotal -= venta.Total;

            _db.Ventas.Remove(venta);
            await _db.SaveChangesAsync();

            res.Message = "Venta eliminada con exito";
            res.Success = true;
            return res;
        }







    }
}
