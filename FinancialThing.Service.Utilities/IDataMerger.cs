using FinancialThing.Models;

namespace FinancialThing.Utilities
{
    public interface IDataMerger<T>
        where T: class, IEntity
    {
        void Merge(T oldCompany, T newCompany);
    }
}