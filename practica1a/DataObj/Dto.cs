using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace practica1a.Models
{
    public class Dto
    {


        public int Id { get; set; }

        public int Ndt1 { get; set; }
        public int? Ndt2 { get; set; }
        public int Ndt3 { get; set; }
        public int Ndt4 { get; set; }
        public int Ndt5 { get; set; }
        public int Ndt6 { get; set; }
        public int Ndt7 { get; set; }
        public int Ndt8 { get; set; }
        public int Ndt9 { get; set; }
        public int Ndt10 { get; set; }

        public string Dt1 { get; set; }
        public string Dt2 { get; set; }
        public string Dt3 { get; set; }
        public string Dt4 { get; set; }
        public string Dt5 { get; set; }
        public string Dt6 { get; set; }
        public string Dt7 { get; set; }
        public string Dt8 { get; set; }
        public string Dt9 { get; set; }
        public string Dt10 { get; set; }


        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt1 { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt2 { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt3 { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt4 { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt5 { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt6 { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt7 { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt8 { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt9 { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal Ddt10 { get; set; }


        //public List<T> List { get; set; }


    }
}
