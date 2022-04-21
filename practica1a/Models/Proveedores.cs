using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class Proveedores
    {

        public int Id { get; set; }

        public string Rif { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Empresa { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        #nullable enable
        public virtual List<ProveedorCategoria>? ProveedorCategorias { get; set; }

        public int? Status { get; set; }


    }
}
