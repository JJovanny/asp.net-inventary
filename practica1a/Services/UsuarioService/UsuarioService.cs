using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly practicaDbContext _db;

        public UsuarioService(practicaDbContext db)
        {
            _db = db;

        }

        public async Task<ResponseAction<List<Usuario>>> GetUsuarios()
        {
            var response = new ResponseAction<List<Usuario>>();

            var usuarios = await _db.Usuarios
                .Include(x => x.UsuariosSession.Rols)
                .ToListAsync();

            if (usuarios.Count > 0)
            {
                response.Data = usuarios;
                response.Success = true;
                return response;
            }

            response.Message = "No hay usuarios registrados";
            response.Success = false;
            response.Data = null;
            return response;

        }


        public async Task<ResponseAction<Usuario>> GetUsuario(int id)
        {
            var response = new ResponseAction<Usuario>();
            var usuario = await _db.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                response.Success = false;
                response.Message = $"El usuario con el id {id} no existe";
                return response;
            }

            response.Data = usuario;
            response.Success = true;
            return response;

        }

        public async Task<ResponseAction<Usuario>> PostUsuario(UsuarioDto usuario)
        {

            var usuarioNuevo = new Usuario();
            var usuarioSessionNuevo = new UsuarioSession();
            var rolUsuario = new RolUsuario();

            var rol = await _db.Rols.FindAsync(usuario.RolId);


            var usuarioPorUsername = await _db.Usuarios.FirstOrDefaultAsync(x => x.Username == usuario.Username);

            var resposne = new ResponseAction<Usuario>();

            if (!string.IsNullOrWhiteSpace(usuario.Nombre) && !string.IsNullOrWhiteSpace(usuario.Apellido) && !string.IsNullOrWhiteSpace(usuario.Direccion) && !string.IsNullOrWhiteSpace(usuario.Username) && !string.IsNullOrWhiteSpace(usuario.Password) && !string.IsNullOrWhiteSpace(usuario.Foto))
            {

                if (usuarioPorUsername == null)
                {

                    if (rol != null)
                    {

                        usuarioNuevo.Nombre = usuario.Nombre;
                        usuarioNuevo.Apellido = usuario.Apellido;
                        usuarioNuevo.Direccion = usuario.Direccion;
                        usuarioNuevo.Username = usuario.Username;
                        usuarioNuevo.Password = usuario.Password;
                        usuarioNuevo.Foto = usuario.Foto;

                        _db.Usuarios.Add(usuarioNuevo);
                        await _db.SaveChangesAsync();


                        rolUsuario.RolId = rol.Id;
                        rolUsuario.UsuarioId = usuarioNuevo.Id;
                        _db.RolsUsuarios.Add(rolUsuario);
                        await _db.SaveChangesAsync();


                        usuarioSessionNuevo.UsuarioId = usuarioNuevo.Id;
                        usuarioSessionNuevo.RolId = rol.Id;
                        _db.UsuariosSessions.Add(usuarioSessionNuevo);
                        await _db.SaveChangesAsync();


                        usuarioNuevo.UsuarioSessionId = usuarioSessionNuevo.Id;
                        await _db.SaveChangesAsync();


                        resposne.Message = "Usuario creado";
                        resposne.Success = true;
                        resposne.Data = usuarioNuevo;
                        return resposne;
                    }
                    else
                    {
                        resposne.Message = $"Disculpe, el id rol {usuario.RolId} no existe";
                        resposne.Success = false;
                        return resposne;
                    }


                }
                else
                {
                    resposne.Message = $"Disculpe, el nombre de usuario {usuario.Username} ya existe, porfavor debe de escoger otro";
                    resposne.Success = false;
                    return resposne;
                }


            }


            resposne.Message = "Todos los campos son requeridos";
            resposne.Success = false;
            resposne.Data = null;
            return resposne;


        }

        public async Task<ResponseAction<Usuario>> PutUsuario(int id, UsuarioDto usuario)
        {
            var response = new ResponseAction<Usuario>();

            var UsuarioId = await _db.Usuarios.FindAsync(id);

            var rolUsuario = await _db.RolsUsuarios
                .Where(x => x.UsuarioId == UsuarioId.Id && x.RolId == usuario.RolId)
                .FirstOrDefaultAsync();

            var usuarioSession = await _db.UsuariosSessions.FirstOrDefaultAsync(x => x.UsuarioId == UsuarioId.Id);


            if (UsuarioId == null)
            {
                response.Message = $"El usuario con el id {id} no existe"; response.Success = false; response.Data = null; return response;
            }

            if (rolUsuario != null)
            {

                UsuarioId.Nombre = usuario.Nombre;
                UsuarioId.Apellido = usuario.Apellido;
                UsuarioId.Direccion = usuario.Direccion;
                rolUsuario.RolId = usuario.RolId;
                UsuarioId.Username = usuario.Username;
                UsuarioId.Foto = usuario.Foto;
                await _db.SaveChangesAsync();


                usuarioSession.RolId = rolUsuario.RolId;
                await _db.SaveChangesAsync();


                response.Message = "Usuario actualizado exitosamente";
                response.Success = true;
                response.Data = UsuarioId;
                return response;

            }


            response.Message = "Usuario no posee ese rol";
            response.Success = false;
            return response;

        }


        public async Task<ResponseAction<Usuario>> DeleteUsuario(int id)
        {
            var response = new ResponseAction<Usuario>();

            var user = await _db.Usuarios.FindAsync(id);
            if (user == null)
            {
                response.Message = $"El usuario con el id {id} no existe";
                response.Success = false;
                response.Data = null;
                return response;

            }

            _db.Usuarios.Remove(user);
            await _db.SaveChangesAsync();

            response.Message = $"El usuario con el id {id} ha sido eliminado";
            response.Success = true;
            response.Data = null;
            return response;

        }

        public async Task<ResponseAction<Usuario>> GetUsuarioUsername(string username)
        {


            var usuarioUsername = await _db.Usuarios.FirstOrDefaultAsync(x => x.Username == username);

            var response = new ResponseAction<Usuario>();


            if (usuarioUsername == null)
            {
                response.Message = $"no hay un usuario {usuarioUsername}";
                return response;

            }
            else
            {
                response.Data = usuarioUsername;
                return response;


            }


        }

    }
}
