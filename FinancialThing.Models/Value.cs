using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FinancialThing.Models
{
    public class Value: Entity
    {
        [ScriptIgnore]
        public virtual Data Data { get; set; }
        public virtual decimal DataValue { get; set; }
        public virtual string Year { get; set; }
    }
}
