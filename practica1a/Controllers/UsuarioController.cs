using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.UsuarioService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _service;
        private readonly practicaDbContext _db;

        public UsuarioController(IUsuarioService service, practicaDbContext db)
        {
            _service = service;
            _db = db;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ResponseAction<List<Usuario>>> GetUsuarios()
        {

            var usuarios = await _service.GetUsuarios();
            return usuarios;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet, Route("{id}")]
        public async Task<ResponseAction<Usuario>> GetUsuario(int id)
        {

            var usuario = await _service.GetUsuario(id);

            return usuario;

        }

        [HttpGet,Route("username")]
        public async Task<ResponseAction<Usuario>> GetUsuarioUsername(string username)
        {

           return await _service.GetUsuarioUsername(username);

        }


        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<ResponseAction<Usuario>> PostUsuario(UsuarioDto usuario)
        {

            var user = await _service.PostUsuario(usuario);

            return user;

        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]

        public async Task<ResponseAction<Usuario>> PutUsuario(int id, UsuarioDto usuario)
        {

            var usuarioUpdate = await _service.PutUsuario(id, usuario);

            return usuarioUpdate;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet,Route("Delete/{id}")]
        public async Task<ResponseAction<Usuario>> DeleteUsuario(int id)
        {

            var user = await _service.DeleteUsuario(id);
            return user;
        }














    }
}
