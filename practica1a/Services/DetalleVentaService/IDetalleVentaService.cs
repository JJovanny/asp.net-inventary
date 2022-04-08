using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.DetalleVentaService
{
    public interface IDetalleVentaService
    {
        Task<IEnumerable<DetalleVenta>> GetDetalleVentas();

        Task<ResponseAction<List<DetalleVenta>>> GetDetalleVenta(int id);

        Task<ResponseAction<DetalleVenta>> PostDetalleVenta(DetalleVentaDto detalleVentaDto);

        Task<ResponseAction<DetalleVenta>> PutDetalleVenta(int id, DetalleVentaDto detalleVentaDto);

        Task<ResponseAction<DetalleVenta>> DeleteDetalleVenta(int id);



    }
}
