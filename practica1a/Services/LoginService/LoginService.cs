using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace practica1a.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly practicaDbContext _db;
        private readonly IConfiguration _configuration;

        public LoginService(practicaDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;


        }

        public async Task<Dto> AutenticarUsuario(Dto login)
        {

            var usuario = await _db.Usuarios
                .Include(x => x.UsuariosSession)
                .Include(x => x.UsuariosSession.Rols)
                .FirstOrDefaultAsync(x => x.Username == login.Dt1 );

            bool passwordVerify = BCrypt.Net.BCrypt.Verify(login.Dt2, usuario.Password);

            if (usuario == null)
            {
                return null;
            }
            else
            {

                if (passwordVerify)
                {
                    return new Dto()
                    {
                        Ndt1 = usuario.Id,
                        Dt1 = usuario.Username,
                        Dt2 = usuario.Password,
                        Dt3 = usuario.Nombre,
                        Dt4 = usuario.Apellido,
                        Dt5 = usuario.Direccion,
                        Dt6 = usuario.UsuariosSession.Rols.Tipo
                    };
                   

                }
                else {
                    return new Dto()
                    {
                        Dt1 = "contraseñaincorrecta",

                    };

                }
            }
        }

        public string GenerarTokenJWT(Dto login)
        {

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var _Header = new JwtHeader(credentials);

            var usuario = _db.Usuarios
                .Include(x => x.UsuariosSession.Rols)
                .FirstOrDefault(u => u.Username == login.Dt1);


            var _Claims = new[]{

            new Claim(JwtRegisteredClaimNames.NameId, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Username),
            new Claim(ClaimTypes.Role, usuario.UsuariosSession.Rols.Tipo),

        };

            var _Payload = new JwtPayload(

                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audiencie"],
                claims: _Claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(24)
                );


            var _Token = new JwtSecurityToken(_Header, _Payload);

            return new JwtSecurityTokenHandler().WriteToken(_Token);


        }


        public async Task<ResponseAction<Dto>> RegistrarUsuario(Dto usuarioNuevo)
        {
            var res = new ResponseAction<Dto>(); 
            var usuario = new Usuario();
            var rolUsuario = new RolUsuario();
            var usuarioSessionNuevo = new UsuarioSession();

            if(!string.IsNullOrEmpty(usuarioNuevo.Dt1) && !string.IsNullOrEmpty(usuarioNuevo.Dt2) && !string.IsNullOrEmpty(usuarioNuevo.Dt3) && !string.IsNullOrEmpty(usuarioNuevo.Dt4) && !string.IsNullOrEmpty(usuarioNuevo.Dt5))
            {
                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var passwordEncrypt = BCrypt.Net.BCrypt.HashPassword(usuarioNuevo.Dt5, salt);

                usuario.Nombre = usuarioNuevo.Dt1;
                usuario.Apellido = usuarioNuevo.Dt2;
                usuario.Direccion = usuarioNuevo.Dt3;
                usuario.Username = usuarioNuevo.Dt4;
                usuario.Password = passwordEncrypt;

                //agregamos nuevo usuario
                _db.Usuarios.Add(usuario);
               await _db.SaveChangesAsync();

                // agregamos a la tabla intermedia el usuarioy el rol por default que es cliente
                rolUsuario.UsuarioId = usuario.Id;
                rolUsuario.RolId = 2;

                _db.RolsUsuarios.Add(rolUsuario);
                await _db.SaveChangesAsync();

                // agregamos la nueva session con el usuario recien creado y su rol
                usuarioSessionNuevo.UsuarioId = usuario.Id;
                usuarioSessionNuevo.RolId = rolUsuario.RolId;

                _db.UsuariosSessions.Add(usuarioSessionNuevo);
                await _db.SaveChangesAsync();

                // actualizamos el campo usuarioSessionId delusaurio,con la session creada
                usuario.UsuarioSessionId = usuarioSessionNuevo.Id;
                await _db.SaveChangesAsync();



                res.Message = "Perfecto, Te has registrado exitosamente";
                res.Data = new Dto()
                {
                    Ndt1 = usuario.Id,
                    Dt1 = usuario.Nombre,
                    Dt2 = usuario.Apellido,
                    Dt3 = usuario.Direccion,
                    Dt4 = usuario.Username,
                    Dt5 = usuario.Password,

            };

                return res;

            }
            else
            {

                res.Message = "Disculpe, Todos los campos son requeridos";
                return res;

            }


        }





    }
}
