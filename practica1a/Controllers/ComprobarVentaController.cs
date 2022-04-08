using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services.ComprobarVentaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComprobarVentaController : ControllerBase
    {

        private readonly IComprobarVentaService _service;

        // private readonly practicaDbContext _db;

        public ComprobarVentaController(IComprobarVentaService service)
        {
            _service = service;
            
        }

        //[HttpGet]
        //public async Task<IEnumerable<OrdenCompra>> GetOdenCompras()
        //{
        //    return await _service.GetOdenCompras();
        //}


        [HttpPut("{id}")]
        public async Task<ActionResult> ComprobarOrdenCompra(ComprobarVentaDto comprobar)
        {

            return await _service.ComprobarOrdenCompra(comprobar.id, comprobar.verificacion);

        }



    }
}
