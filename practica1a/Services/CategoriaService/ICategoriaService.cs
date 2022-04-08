using practica1a.DataObj;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.CategoriaService
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetCategorias();

        Task<ResponseAction<IEnumerable<Categoria>>> GetCategoria(int id);

        Task<ResponseAction<Categoria>> PostCategoria(CategoriaDto rol);

        Task<ResponseAction<Categoria>> PutCategoria(int id, CategoriaDto rol);

        Task<ResponseAction<Categoria>> DeleteCategoria(int id);


    }
}
