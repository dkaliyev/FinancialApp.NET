using System;
using System.Runtime.InteropServices.ComTypes;
using FinancialThing.Models;

namespace FinancialThing.DataAccess
{
    public interface ICompanyServiceRepository : IRepository<Company, Guid>
    {
         
    }
}