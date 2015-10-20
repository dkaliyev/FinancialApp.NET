using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class Operations: Entity
    {
        public Data NetIncome { get; set; }
        public Data Depreciation { get; set; }
        public Data NonCashItems { get; set; }
        public Data CashTaxesPaid { get; set; }
        public Data CashInterestPaid { get; set; }
        public Data ChangesInCapital { get; set; }
        public Data TotalCashFromOperations { get; set; }
    }
}
