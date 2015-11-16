using System.Collections.Generic;

namespace FinancialThing.Models
{
    public class Financials: Entity
    {
        public virtual ISet<Page> Pages { get; set; } 
        //public virtual BalanceSheet BalanceSheet { get; set; }
        //public virtual CashFlow CashFlow { get; set; }
        //public virtual IncomeStatement IncomeStatement { get; set; }
        public virtual Company Company { get; set; }

        public virtual int MinYear { get; set; }
        public virtual int MaxYear { get; set; }
    }
}