using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.CompraService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {

        private readonly ICompraService _service;

        public CompraController(ICompraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Compra>> GetCompras()
        {

            return await _service.GetCompras();

        }



        [HttpGet("{id}")]
        public async Task<ResponseAction<Compra>> GetCompra(int id)
        {
            return await _service.GetCompra(id);

        }



        [HttpPost]
        public async Task<ResponseAction<Compra>> PostCompra(CompraDto compraNew)
        {

            return await _service.PostCompra(compraNew);

        }


        [HttpPut("{id}")]
        public async Task<ResponseAction<Compra>> PutCompra(int id, CompraDto compraNew)
        {

            return await _service.PutCompra(id, compraNew);

        }


        [HttpDelete("{id}")]
        public async Task<ResponseAction<Compra>> DeleteCompra(int id)
        {
            return await _service.DeleteCompra(id);

        }








    }
}
