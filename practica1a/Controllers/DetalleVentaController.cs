using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.DetalleVentaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {

        private readonly IDetalleVentaService _service;
        public DetalleVentaController(IDetalleVentaService service)
        {
            _service = service;

        }

        [HttpGet]
        public async Task<IEnumerable<DetalleVenta>> GetDetalleVentas()
        {

            return await _service.GetDetalleVentas();

        }


        [HttpGet("{id}")]
        public async Task<ResponseAction<List<DetalleVenta>>> GetDetalleVenta(int id)
        {

            return await _service.GetDetalleVenta(id);

        }


        [HttpPost]
        public async Task<ResponseAction<DetalleVenta>> PostDetalleVenta(DetalleVentaDto detalleObj)
        {
            return await _service.PostDetalleVenta(detalleObj);

        }



        [HttpPut("{id}")]
        public async Task<ResponseAction<DetalleVenta>> PutDetalleVenta(int id, DetalleVentaDto detalleObj)
        {
            return await _service.PutDetalleVenta(id, detalleObj);

        }


        [HttpDelete("{id}")]
        public async Task<ResponseAction<DetalleVenta>> DeleteDetalleVenta(int id)
        {

            return await _service.DeleteDetalleVenta(id);

        }






    }
}
