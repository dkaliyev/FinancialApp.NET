using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class Share: Entity
    {
        public virtual double Open { get; set; }
        public virtual double Close { get; set; }
        public virtual double High { get; set; }
        public virtual double Low { get;set; }
        public virtual double Volume { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual FEntity FEntity { get; set; }
    }
}
