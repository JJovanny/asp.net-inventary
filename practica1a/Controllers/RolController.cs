using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.RolService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {

        private readonly IRolService _service;

        public RolController(IRolService service)
        {
            _service = service;
        }



        [HttpGet]
        public async Task<IEnumerable<Rol>> GetRoles()
        {

            return await _service.GetRoles();

        }



        [HttpGet, Route("RolUsuario")]
        public async Task<ResponseAction<IEnumerable<Rol>>> GetRolesUsuarios(int id)
        {

            return await _service.GetRolesUsuarios(id);


        }


        [HttpPost]
        public async Task<ResponseAction<Rol>> PostRol(RolDto rol)
        {
            return await _service.PostRol(rol);


        }


        [HttpPut("{id}")]
        public async Task<ResponseAction<Rol>> PutRol(int id, RolDto rolUpdate)
        {

            return await _service.PutRol(id, rolUpdate);


        }


        [HttpDelete("id")]
        public async Task<ResponseAction<Rol>> DeleteRol(int id)
        {

            return await _service.DeleteRol(id);


        }







    }
}
