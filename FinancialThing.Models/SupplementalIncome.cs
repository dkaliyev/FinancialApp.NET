using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class SupplementalIncome: Entity
    {
        public Data Depreciation { get; set; }
        public Data CashInterestPaid { get; set; }
        public Data CashTaxesPaid { get; set; }
    }
}
