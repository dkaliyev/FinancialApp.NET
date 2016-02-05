using FinancialThing.Models;

namespace FinancialThing.DataAccess
{
    public interface IDatabaseRepository<T, K>: IRepository<T, K>
        where T : class, IEntity
    {
         
    }
}