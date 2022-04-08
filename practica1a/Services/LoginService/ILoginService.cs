using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.LoginService
{
    public interface ILoginService
    {

        Task<Dto> AutenticarUsuario(Dto login);

        string GenerarTokenJWT(Dto login);

        Task<ResponseAction<Dto>> RegistrarUsuario(Dto usuarioNuevo);


    }
}
