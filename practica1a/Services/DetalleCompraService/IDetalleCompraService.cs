using Microsoft.AspNetCore.Http;
using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.DetalleCompraService
{
    public interface IDetalleCompraService
    {
        Task<IEnumerable<DetalleCompra>> GetDetalleCompras();

        Task<ResponseAction<DetalleCompra>> GetDetalleCompra(int id);

        Task<ResponseAction<DetalleCompra>> PostDetalleCompra(Dto fecha);

        Task<ResponseAction<DetalleCompra>> EnviarComprobanteDePago(DetalleCompraDto deta);
      //  Task<ResponseAction<DetalleCompra>> EnviarComprobanteDePago(int id, byte[] file);

        Task<ResponseAction<DetalleCompra>> DeleteDetalleCompra(int id);


    }
}
