using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class DetalleCompra
    {

        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuario Usuarios { get; set; }

        public string Fecha { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Subtotal { get; set; }

    #nullable enable
        public virtual List<Compra>? Compras { get; set; }

        public string? Archivo { get; set; }

        public string? Verificada { get; set; }

        public int? Status { get; set; }


    }
}
