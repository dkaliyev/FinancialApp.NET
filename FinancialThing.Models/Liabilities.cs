using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class Liabilities: StatementClass
    {
        public Data AccountsPayable { get; set; }
        public Data AccruedExpenses { get; set; }
        public Data NotesPayable { get; set; }
        public Data CurrentPortion { get; set; }
        public Data OtherCurrentLiabTotal { get; set; }
        public Data TotalCurrentLiab { get; set; }
        public Data TotalLongTermDebt { get; set; }
        public Data TotalDebt { get; set; }
        public Data DeferredIncomeTax { get; set; }
        public Data MinorityInterest { get; set; }
        public Data OtherLiabilitiesTotal { get; set; }
        public Data TotalLiabilities { get; set; }
    }
}
