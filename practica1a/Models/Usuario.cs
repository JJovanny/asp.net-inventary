using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class Usuario
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Direccion { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Foto { get; set; } = null;

       #nullable enable

        public virtual List<RolUsuario>? RolsUsuarios { get; set; }

        public int? UsuarioSessionId { get; set; }

        public UsuarioSession? UsuariosSession { get; set; }

        public virtual List<Direcciones>? Direcciones { get; set; }

        public int? Status { get; set; }

    }
}
