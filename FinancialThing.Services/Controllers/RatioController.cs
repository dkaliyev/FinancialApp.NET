using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;

namespace FinancialThing.Services.Controllers
{

    public class RatioController : ApiController
    {
        private IDatabaseRepository<Ratio, Guid> _ratioRepository;

        private IUnitOfWork _uow;

        public RatioController(IDatabaseRepository<Ratio, Guid> repo, IUnitOfWork uow)
        {
            _ratioRepository = repo;
            _uow = uow;
        }

        public HttpResponseMessage Get(Guid id)
        {
            var result = _ratioRepository.GetQuery().Where(r => r.Id == id);
            return FTJsonSerializer.Serialize(result);
        }

        public HttpResponseMessage GetAll()
        {
            var result = _ratioRepository.GetQuery().ToList();
            return FTJsonSerializer.Serialize(result);
        }

        [HttpPost]
        public HttpResponseMessage Post(Ratio ratio)
        {
            _ratioRepository.Add(ratio);
            _uow.Commit();
            return FTJsonSerializer.Serialize(ratio);
        }

        [HttpDelete]
        public void Delete(Ratio ratio)
        {
            _ratioRepository.Delete(ratio);
            _uow.Commit();
        }
    }
}
