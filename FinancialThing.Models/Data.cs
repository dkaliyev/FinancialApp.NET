using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FinancialThing.Models
{
    public class Data: Entity
    {
        [ScriptIgnore]
        public virtual StatementClass StatementClass { get; set; }
        public virtual Dictionary Dictionary { get; set; }
        public virtual ISet<Value> Values { get; set; }

    }
}
