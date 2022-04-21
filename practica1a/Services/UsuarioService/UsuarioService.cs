using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace practica1a.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly practicaDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UsuarioService(practicaDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;


        }

        public async Task<ResponseAction<List<Usuario>>> GetUsuarios()
        {
            var response = new ResponseAction<List<Usuario>>();

            var usuarios = await _db.Usuarios
                .Include(x => x.UsuariosSession.Rols)
                .Where(x => x.Status != 0 || x.Status == null)
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

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordEncrypt = BCrypt.Net.BCrypt.HashPassword(usuario.Password, salt);

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
                        usuarioNuevo.Password = passwordEncrypt;
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

                        var UsuarioId = await _db.Usuarios.
                         Include(x => x.UsuariosSession.Rols)
                         .FirstOrDefaultAsync(x => x.Id == usuarioNuevo.Id);

                        resposne.Message = "Usuario creado";
                        resposne.Success = true;
                        resposne.Data = UsuarioId;
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

            var UsuarioId = await _db.Usuarios.
                Include(x => x.UsuariosSession.Rols)
                .FirstOrDefaultAsync(x => x.Id == id);

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

            var usuarioEliminado = await _db.Usuarios.FindAsync(id);

            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var historialUsuariosEliminados = new HistorialUsuariosEliminados();
            var userClaims = identity.Claims;
            int.TryParse(userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value, out var idusuario);
            
            var usuarioSession = await _db.Usuarios.FirstOrDefaultAsync(x => x.Id == idusuario);
            DateTime dateTime = DateTime.Now;

            

            if (usuarioEliminado == null)
            {
                response.Message = $"El usuario con el id {id} no existe";
                response.Success = false;
                response.Data = null;
                return response;

            }

            usuarioEliminado.Status = 0;

            historialUsuariosEliminados.UsuarioId = usuarioSession.Id;
            historialUsuariosEliminados.NombreUsuario = usuarioSession.Nombre;

            historialUsuariosEliminados.IdUsuarioEliminado = usuarioEliminado.Id;
            historialUsuariosEliminados.NombreUsuarioEliminado = usuarioEliminado.Nombre;

            historialUsuariosEliminados.Fecha = dateTime.ToString("yyyy/MM/dd hh:mm:ss");
            
            _db.HistorialUsuariosEliminados.Add(historialUsuariosEliminados);
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
