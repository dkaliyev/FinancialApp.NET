using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using FinancialThing.DataAccess;
using FinancialThing.DataAccess.nHibernate;
using FinancialThing.Models;
using FinancialThing.Services.Utilities;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using NHibernate.Driver;
using NHibernate.Linq;

namespace FinancialThing.Services.Controllers
{
    public class DictionaryController : ApiController
    {
        private readonly IRepository<Dictionary, Guid> _repo;
        private readonly IRepository<Data, Guid> _dataRepo;
        private IUnitOfWork _uow;
        public DictionaryController(IRepository<Dictionary, Guid> dictionaryRepository, IRepository<Data, Guid> dataRepository, IUnitOfWork uow)
        {
            _repo = dictionaryRepository;
            _dataRepo = dataRepository;
            _uow = uow;
        }
        // GET api/values
        public IEnumerable<Dictionary> Get()
        {
            var dic = _repo.FindBy(x => x.Code == "LTI");

            var data = new Data()
            {
                Dictionary = dic
            };

            _dataRepo.Add(data);

            _uow.Commit();

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
        public void Post()
        {
            DictionaryHelper.InsertDicionaries(_repo);
            _uow.Commit();
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
