using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.CompraService
{
    public interface ICompraService
    {

    Task<IEnumerable<Compra>> GetCompras();

    Task<ResponseAction<Compra>> GetCompra(int id);

    Task<ResponseAction<Compra>> PostCompra(CompraDto compraDto);

    Task<ResponseAction<Compra>> PutCompra(int id, CompraDto compraDto);

    Task<ResponseAction<Compra>> DeleteCompra(int id);


    }


}
