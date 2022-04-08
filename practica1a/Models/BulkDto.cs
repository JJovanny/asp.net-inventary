using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class BulkDto : Dto
    {
        public List<Dto> DtoLis1 { get; set; }
        public List<Dto> DtoLis2 { get; set; }
        public List<Dto> DtoLis3 { get; set; }
        public List<Dto> DtoLis4 { get; set; }
        public List<Dto> DtoLis5 { get; set; }
    }
}
