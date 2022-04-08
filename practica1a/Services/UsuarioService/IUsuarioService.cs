using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.UsuarioService
{
    public interface IUsuarioService
    {

        Task<ResponseAction<List<Usuario>>> GetUsuarios();

        Task<ResponseAction<Usuario>> GetUsuario(int id);

        Task<ResponseAction<Usuario>> GetUsuarioUsername(string username);

        Task<ResponseAction<Usuario>> PostUsuario(UsuarioDto usaurio);

        Task<ResponseAction<Usuario>> PutUsuario(int id, UsuarioDto usuario);

        Task<ResponseAction<Usuario>> DeleteUsuario(int id);


    }
}
