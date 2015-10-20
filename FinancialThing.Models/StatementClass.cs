using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class StatementClass: Entity
    {
        public virtual ISet<Data> Data { get; set; } 
        public virtual Dictionary Dictionary { get; set; }
        public virtual Page Page { get; set; }
    }
}
