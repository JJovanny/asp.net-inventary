using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.CategoriaService
{
    public class CategoriaService : ICategoriaService
    {

        private readonly practicaDbContext _db;

        public CategoriaService(practicaDbContext db)
        {
            _db = db;
        }


        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _db.Categorias.ToListAsync();

        }

        public async Task<ResponseAction<IEnumerable<Categoria>>> GetCategoria(int id)
        {
            var res = new ResponseAction<IEnumerable<Categoria>>();

            var Icategoria = await _db.Categorias.
            Where(c => c.Id == id)
            .ToListAsync();

            if (Icategoria == null)
            {
                res.Message = $"la categoria con el id {id} no existe";
                res.Data = null;
                return res;
            }

            res.Data = Icategoria;
            res.Success = true;
            return res;

        }


        public async Task<ResponseAction<Categoria>> PostCategoria(CategoriaDto categoriDto)
        {

            var res = new ResponseAction<Categoria>();
            var categoria = new Categoria();

            if (string.IsNullOrEmpty(categoriDto.TipoCategoria))
            {
                res.Message = "Disculpe el campo tipo categoria es requerido";
                return res;
            }
            else
            {
                categoria.TipoCategoria = categoriDto.TipoCategoria;
                _db.Categorias.Add(categoria);
                await _db.SaveChangesAsync();


                res.Message = "Categoria creada con exito";
                res.Success = true;
                return res;
            }


        }


        public async Task<ResponseAction<Categoria>> PutCategoria(int id, CategoriaDto categoriaDto)
        {

            var res = new ResponseAction<Categoria>();
            var Icategoria = await _db.Categorias.FindAsync(id);

            if (Icategoria == null)
            {
                res.Message = $"No existe una categoria con el id {id}";
                res.Data = null;
                return res;
            }

            Icategoria.TipoCategoria = categoriaDto.TipoCategoria;
            await _db.SaveChangesAsync();

            res.Message = "Categoria actualizada";
            res.Success = true;
            return res;


        }

        public async Task<ResponseAction<Categoria>> DeleteCategoria(int id)
        {
            var res = new ResponseAction<Categoria>();

            var Icategoria = await _db.Categorias.FindAsync(id);

            if (Icategoria == null)
            {
                res.Message = $"la categoria con el id {id} no existe";
                res.Data = null;
                return res;
            }

            _db.Categorias.Remove(Icategoria);
            await _db.SaveChangesAsync();

            res.Data = Icategoria;
            res.Success = true;
            return res;
        }






    }
}
