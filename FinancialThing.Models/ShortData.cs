using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class ShortData
    {
        public Dictionary Dictionary { get; set; }
        public IEnumerable<ShortValue> Values { get; set; }
    }
}
