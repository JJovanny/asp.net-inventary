using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.ProductoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {


        private readonly IProductoService _service;

        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Cliente, Admin")]
        [HttpGet]
        public async Task<IEnumerable<Producto>> GetProductos()
        {

            return await _service.GetProductos();


        }

        [Authorize(Roles = "Admin")]
        [HttpGet, Route("nombre")]
        public async Task<ResponseAction<List<Producto>>> GetProductoNombre(string nombre)
        {

            return await _service.GetProductoNombre(nombre);


        }

        [Authorize(Roles = "Admin")]
        [HttpGet, Route("{id}")]
        public async Task<ResponseAction<Producto>> GetProducto(int id)
        {

            return await _service.GetProducto(id);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ResponseAction<Producto>> PostProducto(ProductoDto producto)
        {
            return await _service.PostProducto(producto);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ResponseAction<Producto>> PutProducto(int id, ProductoDto putProducto)
        {

            return await _service.PutProducto(id, putProducto);

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public async Task<ResponseAction<Producto>> DeleteProducto(int id)
        {

            return await _service.DeleteProducto(id);

        }


    }
}
