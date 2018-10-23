using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomsoftZadatak.Models
{
    class StatementItem
    {
        public string Artikl_uid { get; set; }
        public string Naziv_artikla { get; set; }
        public float Kolicina { get; set; }
        public float Iznos { get; set; }
        public char Usluga { get; set; }
    }
}
