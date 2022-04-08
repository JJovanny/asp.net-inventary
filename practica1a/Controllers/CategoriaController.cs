using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1a.DataObj;
using practica1a.Models;
using practica1a.Services;
using practica1a.Services.CategoriaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _service.GetCategorias();

        }


        [HttpGet("{id}")]
        public async Task<ResponseAction<IEnumerable<Categoria>>> GetCategoria(int id)
        {

            return await _service.GetCategoria(id);

        }

        [HttpPost]
        public async Task<ResponseAction<Categoria>> PostCategoria(CategoriaDto categoria)
        {
            return await _service.PostCategoria(categoria);

        }



        [HttpPut("{id}")]
        public async Task<ResponseAction<Categoria>> PutCategoria(int id, CategoriaDto categoria)
        {

            return await _service.PutCategoria(id, categoria);

        }


        [HttpDelete("{id}")]
        public async Task<ResponseAction<Categoria>> DeleteCategoria(int id)
        {

            return await _service.DeleteCategoria(id);

        }




    }
}
