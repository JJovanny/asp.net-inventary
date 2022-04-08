using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.DetalleCompraService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleCompraController : ControllerBase
    {

        private readonly IDetalleCompraService _service;
        private readonly practicaDbContext _db;

        public DetalleCompraController(IDetalleCompraService service, practicaDbContext db)
        {
            _service = service;
            _db = db;
        }



        [HttpGet]
        public async Task<IEnumerable<DetalleCompra>> GetDetalleCompras()
        {

            return await _service.GetDetalleCompras();

        }


        [HttpGet("{id}")]
        public async Task<ResponseAction<DetalleCompra>> GetDetalleCompra(int id)
        {

            return await _service.GetDetalleCompra(id);

        }


        [HttpPost]
        public async Task<ResponseAction<DetalleCompra>> PostDetalleCompra(Dto fecha)
        {
            return await _service.PostDetalleCompra(fecha);

        }



        //[HttpPut("{id}")]
        //public async Task<ResponseAction<DetalleCompra>> PutEnviarComprobanteDePago(int id, byte[] file)
        //{
        //    return await _service.EnviarComprobanteDePago(id,file);

        //}



        [HttpPut("{id}")]
        public async Task<ResponseAction<DetalleCompra>> PutEnviarComprobanteDePago(DetalleCompraDto deta)
        {
            return await _service.EnviarComprobanteDePago(deta);

        }


        [HttpDelete("{id}")]
        public async Task<ResponseAction<DetalleCompra>> DeleteDetalleCompra(int id)
        {

            return await _service.DeleteDetalleCompra(id);

        }




    }
}
