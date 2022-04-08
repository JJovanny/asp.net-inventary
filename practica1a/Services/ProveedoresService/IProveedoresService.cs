using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.ProveedoresService
{
   public interface IProveedoresService
    {

        Task<List<Dto>> GetProveedores();

        Task<ResponseAction<Dto>> GetProveedor(int Id);

        Task<ResponseAction<Dto>> PostProveedor(Dto dto);

        Task<ResponseAction<Dto>> PutProveedor(int id, Dto dto);

        Task<ResponseAction<Dto>> DeleteProveedor(int Id);


    }
}
