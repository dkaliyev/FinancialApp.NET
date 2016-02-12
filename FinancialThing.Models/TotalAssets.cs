using System;

namespace FinancialThing.Models
{
    public class TotalAssets:Entity
    {
        public virtual int Year { get; set; }
        public virtual Financials Financials { get; set; }
        public virtual float Value { get; set; }
    }
}