using FinancialThing.Models;

namespace FinancialThing.Utilities
{
    public interface IParser<T>
        where T: class, IEntity
    {
        T Parse(string name, string exchange);
    }
}