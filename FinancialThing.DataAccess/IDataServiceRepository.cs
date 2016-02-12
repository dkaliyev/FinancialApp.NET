using System;
using FinancialThing.Models;

namespace FinancialThing.DataAccess
{
    public interface IDataServiceRepository: IRepository<Company, Guid>
    {
         
    }
}