using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class RolUsuario
    {

        //public int Id { get; set; }

        public int UsuarioId { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }


    }
}
