using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class OrdenCompra
    {
        public int Id { get; set; }

        public int DetalleCompraId { get; set; }

        public virtual DetalleCompra DetalleCompra { get; set; }

        public string Archivo { get; set; }

        public string verificada { get; set; }

#nullable enable
        public virtual Usuario? Usuarios { get; set; }

        public int UsuarioId { get; set; }
    }
}
