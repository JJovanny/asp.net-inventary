using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class HistorialDetalleVentaEliminada
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string NombreUsuario { get; set; }

        public string Fecha { get; set; }

        public int IdDetalleVentaEliminada { get; set; }

    }
}
