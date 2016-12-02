using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialThing.Models;

namespace FinancialThing.Utilities
{
    public interface ICompanyParser<T>
        where T: IEntity
    {
        IEnumerable<T> Parse(string data);
    }
}
