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
using FinancialThing.Utilities;

namespace FinancialThing.Services.Controllers
{
    public class DictionaryController : ApiController
    {
        private readonly IDatabaseRepository<Dictionary, Guid> _repo;
        private IUnitOfWork _uow;
        public DictionaryController(IDatabaseRepository<Dictionary, Guid> dictionaryRepository, IUnitOfWork uow)
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
                    Data = FTJsonSerializer.Serialize(dics),
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
        public Status Post()
        {
            try
            {
                DictionaryHelper.InsertDicionaries(_repo);
                _uow.Commit();
                return new Status
                {
                    Data = "Dictionary is created",
                    StatusCode = "0"
                };
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
