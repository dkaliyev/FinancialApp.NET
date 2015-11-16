using System;

namespace FinancialThing.Models
{
    public class StockExchange: Entity     
    {
        public virtual string Marker { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual DateTime DateAdded { get; set; } 
    }
}