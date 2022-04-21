using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class Venta
    {
        public int Id { get; set; }


        [JsonIgnore]
#nullable enable
        public virtual DetalleVenta? DetalleVenta { get; set; }

        public int DetalleVentaId { get; set; }

        public virtual Producto? Producto { get; set; }

        public int ProductoId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Total { get; set; }

        public int? Status { get; set; }


    }
}
