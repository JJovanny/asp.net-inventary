using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Services
{
    public class ResponseAction<T>
    {
        public string Message { get; set; } = string.Empty;

#nullable enable
        public T? Data { get; set; }

        public bool Success { get; set; } = false;



    }
}
