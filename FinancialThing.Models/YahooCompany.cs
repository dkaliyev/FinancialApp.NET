using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class YahooCompany: Entity
    {
        public virtual string FullName { get; set; }
        public virtual string Symbol { get; set; }
    }
}
