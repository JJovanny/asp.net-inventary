using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.ProductoService
{
    public interface IProductoService
    {

        Task<IEnumerable<Producto>> GetProductos();

        Task<ResponseAction<Producto>> GetProducto(int id);

        Task<ResponseAction<Producto>> PostProducto(ProductoDto productoDto);

        Task<ResponseAction<Producto>> PutProducto(int id, ProductoDto productoDto);

        Task<ResponseAction<Producto>> DeleteProducto(int id);

        Task<ResponseAction<List<Producto>>> GetProductoNombre(string producto);


    }
}
