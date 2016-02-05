using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class Page: Entity
    {
        public virtual ISet<StatementClass> Statements { get; set; } 
        public virtual Dictionary Dictionary { get; set; }
        public virtual Financials Financials { get; set; }
        public virtual int MaxYear { get; set; }
        public virtual int MinYear { get; set; }
    }
}
