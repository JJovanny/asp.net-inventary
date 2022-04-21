using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class HistorialProveedorEliminado
    {

        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string NombreUsuario { get; set; }

        public string Fecha { get; set; }

        public int IdProveedorEliminado { get; set; }

        public string NombreProveedorEliminado { get; set; }



    }
}
