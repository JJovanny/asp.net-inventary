using Microsoft.AspNetCore.Mvc;
using practica1a.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services.ComprobarVentaService
{
    public interface IComprobarVentaService
    {
        //Task<IEnumerable<OrdenCompra>> GetOdenCompras();

        Task<ActionResult> ComprobarOrdenCompra(int id, string verificar);


    }
}
