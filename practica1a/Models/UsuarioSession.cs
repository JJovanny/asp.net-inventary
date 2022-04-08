using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class UsuarioSession
    {

        public int Id { get; set; }

        public int UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario Usuarios { get; set; }

        public int RolId { get; set; }

        public Rol Rols { get; set; }


    }
}
