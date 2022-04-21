using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class Direcciones
    {

        public int Id { get; set; }

        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }

        public string Direccion { get; set; }

    }
}
