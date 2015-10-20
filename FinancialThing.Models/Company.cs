using System;
using System.Collections.Generic;

namespace FinancialThing.Models
{
    public class Company: Entity
    {
        public virtual string FullName { get; set; }
        public virtual DateTime DateAdded { get; set; }
        public virtual string StockName { get; set; }
        public virtual StockExchange StockExchange { get; set; }
        public virtual Financials Financials { get; set; }
    }
}