using System;
using System.Collections.Generic;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Services.Utilities;

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
        public IEnumerable<StockExchange> Get()
        {
            var dics = _repo.GetQuery();

            return dics;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(StockExchange exchange)
        {
            if (exchange != null)
            {
                _repo.Add(exchange);
                _uow.Commit();
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