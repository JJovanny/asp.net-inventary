using Microsoft.AspNetCore.Http;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.OrdenCompraService
{
    public interface IOrdenCompraService
    {
        Task<IEnumerable<OrdenCompra>> GetOdenCompras();

        Task<ResponseAction<OrdenCompra>> GetOdenCompra(int id);

        Task<ResponseAction<OrdenCompra>> PostOdenCompra(int detalleCompra, IFormFile file);


    }
}
