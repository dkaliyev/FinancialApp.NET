using FinancialThing.Models;
using System.Threading.Tasks;

namespace FinancialThing.Utilities
{
    public interface IParser<T, E>
        where T: class, IEntity
        where E: class, IEntity
    {
        Task<T> Parse(string name, E exchange);
        Task<string> GetCompanyName(string name, E exchange);
    }
}