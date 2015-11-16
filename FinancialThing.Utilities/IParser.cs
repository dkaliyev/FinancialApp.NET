using FinancialThing.Models;

namespace FinancialThing.Utilities
{
    public interface IParser<T, E>
        where T: class, IEntity
        where E: class, IEntity
    {
        T Parse(string name, E exchange);
        string GetCompanyName(string name, E exchange);
    }
}