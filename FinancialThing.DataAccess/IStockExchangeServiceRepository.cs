using System;
using FinancialThing.Models;

namespace FinancialThing.DataAccess
{
    public interface IStockExchangeServiceRepository: IRepository<StockExchange, Guid>
    {
         
    }
}