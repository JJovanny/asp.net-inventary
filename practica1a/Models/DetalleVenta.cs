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

        public string NombreCliente { get; set; }

        public string ApellidoCliente { get; set; }

        public string CedulaCliente { get; set; }

        public string Fecha { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Subtotal { get; set; }

#nullable enable
        public virtual List<Venta>? Ventas { get; set; }

        public int? Status { get; set; }

       #nullable enable
        public virtual Usuario? Usuarios { get; set; }

        public int UsuarioId { get; set; }


    }
}
