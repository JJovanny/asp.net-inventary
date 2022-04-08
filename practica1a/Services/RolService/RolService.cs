using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.RolService
{
    public class RolService : IRolService
    {

        private readonly practicaDbContext _db;

        public RolService(practicaDbContext db)
        {
            _db = db;
        }


        public async Task<IEnumerable<Rol>> GetRoles()
        {

            return await _db.Rols.ToListAsync();

        }

        public async Task<ResponseAction<IEnumerable<Rol>>> GetRolesUsuarios(int id)
        {

            var response = new ResponseAction<IEnumerable<Rol>>();

            if (id == 0)
            {
                response.Message = $"no existe un rol con el id {id}";
                response.Success = false;
                response.Data = null;
                return response;

               
            }
            var rol = await _db.Rols
            .Where(r => r.Id == id)
            .ToListAsync();

            response.Data = rol;
            response.Success = false;
            return response;

        }


        public async Task<ResponseAction<Rol>> PostRol(RolDto rol)
        {
            var role = new Rol();
            var respuesta = new ResponseAction<Rol>();

            if (string.IsNullOrWhiteSpace(rol.TipoRol))
            {
                respuesta.Message = "El campo tipo rol es requerido";
                respuesta.Success = false;
                return respuesta;

            }
            role.Tipo = rol.TipoRol;
            _db.Rols.Add(role);
            await _db.SaveChangesAsync();

            respuesta.Data = role;
            respuesta.Message = "Rol creado exitosamente";
            respuesta.Success = true;
            return respuesta;

        }

        public async Task<ResponseAction<Rol>> PutRol(int id, RolDto rol)
        {
            var res = new ResponseAction<Rol>();
            var Idrol = await _db.Rols.FindAsync(id);

            if (Idrol == null)
            {
                res.Message = $"El rol con el id {id} no existe";
                res.Success = false;
                res.Data = null;
                return res;

            }

            Idrol.Tipo = rol.TipoRol;
            await _db.SaveChangesAsync();

            res.Message = "Rol actualizado con exito";
            res.Data = Idrol;
            res.Success = true;
            return res;


        }


        public async Task<ResponseAction<Rol>> DeleteRol(int id)
        {
            var rolId = await _db.Rols.FindAsync(id);
            var res = new ResponseAction<Rol>();

            if (rolId == null)
            {
                res.Message = $"El rol con el id {id} no existe";
                res.Data = null;
                res.Success = false;
                return res;
            }

            _db.Rols.Remove(rolId);
            await _db.SaveChangesAsync();

            res.Message = "Rol eliminado exitosamente";
            res.Success = true;
            return res;
        }






    }
}
