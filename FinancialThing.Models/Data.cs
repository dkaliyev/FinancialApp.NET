using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class Data: Entity
    {
        public virtual StatementClass StatementClass { get; set; }
        public virtual Dictionary Dictionary { get; set; }
        public virtual ISet<Value> Values { get; set; }

    }
}
