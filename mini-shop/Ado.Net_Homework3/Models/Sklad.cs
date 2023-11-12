using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ado.Net_Homework3.Models
{
    public class Sklad
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Vendor { get; set; }
        public int Quantiny { get; set; }
        public decimal Price { get; set; }
        public DateTime Date_ { get; set; }
          
    }
}
