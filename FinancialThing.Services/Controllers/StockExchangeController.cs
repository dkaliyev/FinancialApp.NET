using System;
using System.Collections.Generic;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Services.Utilities;
using FinancialThing.Utilities;
using System.Linq;

namespace FinancialThing.Services.Controllers
{
    public class StockExchangeController: ApiController
    {
         private readonly IDatabaseRepository<StockExchange, Guid> _repo;
        private IUnitOfWork _uow;
        public StockExchangeController(IDatabaseRepository<StockExchange, Guid> dictionaryRepository, IUnitOfWork uow)
        {
            _repo = dictionaryRepository;
            _uow = uow;
        }
        // GET api/values
        public Status Get()
        {
            try
            {
                var dics = _repo.GetQuery();

                return new Status
                {
                    Data = FTJsonSerializer<IQueryable<StockExchange>>.Serialize(dics),
                    StatusCode = "0"
                };
            }
            catch (Exception)
            {
                return new ErrorStatus();
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Status Post(StockExchange exchange)
        {
            try
            {
                if (exchange != null)
                {
                    _repo.Add(exchange);
                    _uow.Commit();
                    return new Status
                    {
                        Data = "Stock exchange is created",
                        StatusCode = "0"
                    };
                }
                return new ErrorStatus();
            }
            catch(Exception)
            {
                return new ErrorStatus();
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}