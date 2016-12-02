using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class ExpandedCompany: Entity
    {
        public string FullName { get; set; }
        public DateTime DateAdded { get; set; }
        public string StockName { get; set; }
        public StockExchange StockExchange { get; set; }
        public Sector Sector { get; set; }
        public Industry Industry { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }

        //public ISet<TotalAssets> TotalAssets { get; set; }
        //public ISet<TotalRevenue> TotalRevenue { get; set; }
        public List<ShortData> Data { get; set; }
    }
}
