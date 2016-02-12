using System;
using FinancialThing.Models;

namespace FinancialThing.DataAccess
{
    public interface ICategoryEntryServiceRepository: IRepository<CategoryEntry, Guid>
    {
         
    }
}