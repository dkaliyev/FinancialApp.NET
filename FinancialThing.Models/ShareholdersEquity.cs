using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class ShareholdersEquity: StatementClass
    {
        public Data CommonStock { get; set; }   
        public Data AdditionalPaidIn { get; set; }   
        public Data RetainedEarningsData { get; set; }   
        public Data TreasuryStock { get; set; }   
        public Data UnrealizedGainLoss { get; set; }
        public Data OtherEquity { get; set; }
        public Data TotalEquity { get; set; }
        public Data TotalLiabAndShare { get; set; }
        public Data TotalCommonShares { get; set; }
        public Data TreasuryShares { get; set; }
    }
}
