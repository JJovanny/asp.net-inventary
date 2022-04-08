using Microsoft.EntityFrameworkCore;
using practica1a.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace practica1a.Services.scheduled
{
    public class scheduled : ISheduled
    {

        private readonly practicaDbContext _db;

        public scheduled(practicaDbContext db)
        {
            _db = db;
    }

          public async Task CleanCompras()
         {

            var ComprobanteDePagoNull = await _db.DetalleCompras
                .Where(x => x.Archivo == null)
                .ToListAsync();


        }




    }
}
