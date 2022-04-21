using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class HistorialUsuariosEliminados
    {

        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string NombreUsuario { get; set; }

        public string Fecha { get; set; }

        public int IdUsuarioEliminado { get; set; }

        public string NombreUsuarioEliminado { get; set; }

    }
}
