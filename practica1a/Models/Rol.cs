using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class Rol
    {
        public int Id { get; set; }

        public string Tipo { get; set; }


#nullable enable
        [JsonIgnore]
        //public List<Usuario>? Usuarios { get; set; }

        public virtual List<RolUsuario>? RolsUsuarios { get; set; }

    }
}
