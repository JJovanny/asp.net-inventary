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

namespace practica1a.Services.ProveedoresService
{
    public class ProveedoresService : IProveedoresService
    {

        private readonly practicaDbContext _db;
        IHttpContextAccessor _httpContextAccessor;

        public ProveedoresService(practicaDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;


        }

        public async Task<List<Dto>> GetProveedores()
        {

            var proveedores = await _db.Proveedores.
                Where(x => x.Status != 0 || x.Status == null)
                .ToListAsync();

            var dtolis = new List<Dto>();

            foreach(var proveedor in proveedores)
            {
                var dto = new Dto()
                {
                    Id = proveedor.Id,
                    Dt1 = proveedor.Nombres,
                    Dt2 = proveedor.Apellidos,
                    Dt3 = proveedor.Rif,
                    Dt4 = proveedor.Empresa,
                    Dt5 = proveedor.Direccion,
                    Dt6 = proveedor.Telefono,

            };

                dtolis.Add(dto);
            }

            return dtolis;

        }


        public async Task<ResponseAction<Dto>> GetProveedor(int Id)
        {

            var proveedor = await _db.Proveedores.FindAsync(Id);
            var res = new ResponseAction<Dto>();
            var dto = new Dto();

            if(proveedor == null)
            {

                res.Message = "no hay provedor con ese id";
                return res;

            }

            dto.Id = proveedor.Id;
            dto.Dt1 = proveedor.Nombres;
            dto.Dt2 = proveedor.Apellidos;
            dto.Dt3 = proveedor.Rif;
            dto.Dt4 = proveedor.Empresa;
            dto.Dt5 = proveedor.Direccion;
            dto.Dt6 = proveedor.Telefono;

            res.Data = dto;
            return res;

        }



        public async Task<ResponseAction<Dto>> PostProveedor(Dto dto)
        {
            var res = new ResponseAction<Dto>();
            var proveedor = new Proveedores();
            var dtobj = new Dto();

           if(string.IsNullOrEmpty(dto.Dt1) && string.IsNullOrEmpty(dto.Dt2) && string.IsNullOrEmpty(dto.Dt3) && string.IsNullOrEmpty(dto.Dt4) && string.IsNullOrEmpty(dto.Dt5) && string.IsNullOrEmpty(dto.Dt6))
            {

                res.Message = "Todos loscamposson requeridos";
                return res;

            }

            proveedor.Nombres = dto.Dt1;
            proveedor.Apellidos = dto.Dt2;
            proveedor.Rif = dto.Dt3;
            proveedor.Empresa = dto.Dt4;
            proveedor.Telefono = dto.Dt5;
            proveedor.Direccion = dto.Dt6;

            _db.Proveedores.Add(proveedor);
           await _db.SaveChangesAsync();


            dtobj.Id =  proveedor.Id;
            dtobj.Dt1 = proveedor.Nombres;
            dtobj.Dt2 = proveedor.Apellidos;
            dtobj.Dt3 = proveedor.Direccion;
            dtobj.Dt4 = proveedor.Empresa;
            dtobj.Dt5 = proveedor.Rif;
            dtobj.Dt6 = proveedor.Telefono;

            res.Data = dtobj;
            return res;

        }


        public async Task<ResponseAction<Dto>> PutProveedor(int id, Dto dto)
        {

            var proveedor = await _db.Proveedores.FindAsync(id);
            var res = new ResponseAction<Dto>();
            var dtObj = new Dto();


            if(proveedor == null)
            {

                res.Message = $"no eciste elproveedore con elid {id}";
                return res;

            }

            proveedor.Nombres = dto.Dt1;
            proveedor.Apellidos = dto.Dt2;
            proveedor.Rif = dto.Dt3;
            proveedor.Empresa = dto.Dt4;
            proveedor.Telefono = dto.Dt5;
            proveedor.Direccion = dto.Dt6;


            await _db.SaveChangesAsync();


            dtObj.Id = proveedor.Id;
            dtObj.Dt1 = proveedor.Nombres;
            dtObj.Dt2 = proveedor.Apellidos;
            dtObj.Dt3 = proveedor.Direccion;
            dtObj.Dt4 = proveedor.Empresa;
            dtObj.Dt5 = proveedor.Rif;
            dtObj.Dt6 = proveedor.Telefono;

            res.Data = dtObj;
            return res;

        }


        public async Task<ResponseAction<Dto>> DeleteProveedor(int Id)
        {

            var proveedorEliminado = await _db.Proveedores.FindAsync(Id);
            var res = new ResponseAction<Dto>();
            var dto = new Dto();

            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var historialProveedorEliminado = new HistorialProveedorEliminado();
            var userClaims = identity.Claims;
            int.TryParse(userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value, out var idusuario);

            var usuarioSession = await _db.Usuarios.FirstOrDefaultAsync(x => x.Id == idusuario);
            DateTime dateTime = DateTime.Now;


            if (proveedorEliminado == null)
            {

                res.Message = "no hay provedor con ese id";
                return res;

            }

            proveedorEliminado.Status = 0;

            historialProveedorEliminado.UsuarioId = usuarioSession.Id;
            historialProveedorEliminado.NombreUsuario = usuarioSession.Nombre;

            historialProveedorEliminado.IdProveedorEliminado = proveedorEliminado.Id;
            historialProveedorEliminado.NombreProveedorEliminado = proveedorEliminado.Nombres;

            historialProveedorEliminado.Fecha = dateTime.ToString("yyyy/MM/dd hh:mm:ss");

            _db.HistorialProveedorEliminados.Add(historialProveedorEliminado);
            await _db.SaveChangesAsync();


            dto.Ndt1 = 1;

            res.Message = "se ha eliminado con exito 1";
            res.Data = dto;
            res.Success = true;
            return res;

        }



    }
}
