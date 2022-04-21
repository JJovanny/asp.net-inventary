using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class ProveedorCategoria
    {

        public Proveedores Proveedores { get; set; }
        public int ProveedorId { get; set; }


        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }


    }
}
