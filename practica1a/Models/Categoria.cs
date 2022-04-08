using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{

    public class Categoria
    {
        public int Id { get; set; }

        public string TipoCategoria { get; set; }

#nullable enable
       
        //public virtual IEnumerable<Producto>? Productos { get; set; }


    }
}
