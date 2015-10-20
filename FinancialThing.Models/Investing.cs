using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class Investing: Entity
    {
        public Data CapitalExpenditures { get; set; }
        public Data OtherInvestingTotal { get; set; }
        public Data TotalCash { get; set; }
    }
}
