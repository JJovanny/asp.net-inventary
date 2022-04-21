using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class HistorialCategoriaEliminada
    {

        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string NombreUsuario { get; set; }

        public string Fecha { get; set; }

        public int IdCategoriaEliminada { get; set; }

        public string NombreCategoriaEliminada { get; set; }


    }
}
