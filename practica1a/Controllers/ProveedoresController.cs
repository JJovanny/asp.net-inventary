using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.ProveedoresService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {

        private readonly IProveedoresService _service;


        public ProveedoresController(IProveedoresService service)
        {
            _service = service;

        }



        [HttpGet]
        public async Task<List<Dto>> GetProveedores()
        {

            return await _service.GetProveedores();

        }



        [HttpGet("{Id}")]
        public async Task<ResponseAction<Dto>> GetProveedor(int Id)
        {

            return await _service.GetProveedor(Id);

        }



        [HttpPost]
        public async Task<ResponseAction<Dto>> PostProveedor(Dto dto)
        {

            return await _service.PostProveedor(dto);

        }




        [HttpPut("{id}")]
        public async Task<ResponseAction<Dto>> PutProveedor(int id, Dto dto)
        {

            return await _service.PutProveedor(id, dto);


        }





        [HttpDelete("{id}")]
        public async Task<ResponseAction<Dto>> DeleteProveedor(int id)
        {

            return await _service.DeleteProveedor(id);

        }




    }
}
