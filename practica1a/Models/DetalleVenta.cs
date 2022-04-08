using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }

        public virtual Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }

        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Subtotal { get; set; }

#nullable enable
        public virtual List<Venta>? Ventas { get; set; }



    }
}
