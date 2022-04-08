using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.LoginService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _service;


        public LoginController(ILoginService service)
        {
            _service = service;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Dto>> Login(Dto login)
        {

            var usuario = await _service.AutenticarUsuario(login);

            if (usuario != null)
            {
                return Ok(new { token = _service.GenerarTokenJWT(usuario), usuario = usuario });

            }
            else
            {

                return Unauthorized();
            }

        }

        [AllowAnonymous]
        [HttpPost,Route("RegistrarUsuario")]
        public async Task<ResponseAction<Dto>> RegistrarUsuario(Dto usuarioNuevo)
        {


            return await _service.RegistrarUsuario(usuarioNuevo);


        }





    }
}
