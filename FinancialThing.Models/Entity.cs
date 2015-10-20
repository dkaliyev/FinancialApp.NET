using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class Entity:IEntity
    {
        public virtual Guid Id { get; set; }
    }
}
