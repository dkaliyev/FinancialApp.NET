using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class Value: Entity
    {
        public virtual Data Data { get; set; }
        public virtual string DataValue { get; set; }
        public virtual string Year { get; set; }
    }
}
