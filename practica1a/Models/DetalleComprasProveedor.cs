using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class DetalleComprasProveedor
    {

        public int Id { get; set; }

        public string FechaDeCompra { get; set; }

        public Proveedores Proveedores { get; set; }
        public int ProveedoresId { get; set; }


        [Column(TypeName = "decimal(18,2")]
        public decimal Total { get; set; }

#nullable enable
        public List<ComprasProveedor>? ComprasProveedores { get; set; }

        public int? Status { get; set; }


        public virtual Usuario? Usuarios { get; set; }
        public int UsuarioId { get; set; }

    }
}
