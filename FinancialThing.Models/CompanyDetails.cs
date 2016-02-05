using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class CompanyDetails: Entity
    {
        public string FullName { get; set; }
        public string StockName { get; set; }
        public string Code { get; set; }
        public CategoryEntry Sector { get; set; }
        public CategoryEntry Industry { get; set; }
    }
}
