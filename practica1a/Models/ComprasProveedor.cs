using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class ComprasProveedor
    {

        public int Id { get; set; }

        public string Producto { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }


        [Column(TypeName = "decimal(18,2")]
        public decimal SubTotal { get; set; }

        public DetalleComprasProveedor DetalleComprasProveedor { get; set; }
        public int DetalleComprasProveedorId { get; set; }

        public int? Status { get; set; }


    }
}
