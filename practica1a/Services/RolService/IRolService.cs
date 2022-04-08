using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.RolService
{
    public interface IRolService
    {

        Task<IEnumerable<Rol>> GetRoles();

        Task<ResponseAction<IEnumerable<Rol>>> GetRolesUsuarios(int id);

        Task<ResponseAction<Rol>> PostRol(RolDto rol);

        Task<ResponseAction<Rol>> PutRol(int id, RolDto rol);

        Task<ResponseAction<Rol>> DeleteRol(int id);


    }
}
