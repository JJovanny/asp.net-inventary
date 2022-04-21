using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string Foto { get; set; }

#nullable enable
        public virtual Categoria? Categorias { get; set; }

        public int CategoriaId { get; set; }

        public int? Status { get; set; }


    }
}
