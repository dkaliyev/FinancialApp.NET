using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class NewCompany
    {
        public Guid Id { get; set; }
        public string StockName { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; } 
    }
}
