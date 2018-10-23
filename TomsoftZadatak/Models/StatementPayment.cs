using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomsoftZadatak.Models
{
    class StatementPayment
    {
        public string Vrste_placanja_uid { get; set; }
        public string Naziv { get; set; }
        public float Iznos { get; set; }
        public string Nadgrupa_placanja_uid { get; set; }
        public string Nadgrupa_placanja_naziv { get; set; }
    }
}
