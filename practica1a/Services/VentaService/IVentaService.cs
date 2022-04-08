using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.VentaService
{
    public interface IVentaService
    {
        Task<IEnumerable<Venta>> GetVentas();

        Task<ResponseAction<Venta>> GetVenta(int id);

        Task<ResponseAction<Venta>> PostVenta(VentaDto ventaDto);

        Task<ResponseAction<Venta>> PutVenta(int id, VentaDto ventaDto);

        Task<ResponseAction<Venta>> DeleteVenta(int id);


    }
}
