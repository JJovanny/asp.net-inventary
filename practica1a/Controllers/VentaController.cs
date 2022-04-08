using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.VentaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        private readonly IVentaService _service;

        public VentaController(IVentaService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<Venta>> GetVentas()
        {

            return await _service.GetVentas();

        }


        [HttpGet("{id}")]
        public async Task<ResponseAction<Venta>> GetVenta(int id)
        {
            return await _service.GetVenta(id);

        }


        [HttpPost]
        public async Task<ResponseAction<Venta>> PostVenta(VentaDto ventaNew)
        {

            return await _service.PostVenta(ventaNew);

        }


        [HttpPut("{id}")]
        public async Task<ResponseAction<Venta>> PutVenta(int id, VentaDto ventaUpdate)
        {

            return await _service.PutVenta(id, ventaUpdate);

        }


        [HttpDelete("{id}")]
        public async Task<ResponseAction<Venta>> DeleteVenta(int id)
        {
            return await _service.DeleteVenta(id);

        }








    }
}
