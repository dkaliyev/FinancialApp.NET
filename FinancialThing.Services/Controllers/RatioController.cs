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

        public Status Get(Guid id)
        {
            try
            {
                var result = _ratioRepository.GetQuery().Where(r => r.Id == id);
                var data = FTJsonSerializer.Serialize(result);
                return new Status
                {
                    Data = data,
                    StatusCode = "0"
                };
            }
            catch(Exception)
            {
                return new Status
                {
                    Data = "There was an error",
                    StatusCode = "1"
                };
            }
        }

        public Status GetAll()
        {
            try
            {
                var result = _ratioRepository.GetQuery().ToList();
                var data = FTJsonSerializer.Serialize(result);
                return new Status
                {
                    Data = data,
                    StatusCode = "0"
                };
            }
            catch(Exception)
            {
                return new ErrorStatus();
            }
        }

        [HttpPost]
        public Status Post(Ratio ratio)
        { 
            try
            {
                _ratioRepository.Add(ratio);
                _uow.Commit();
                var data = FTJsonSerializer.Serialize(ratio);
                return new Status
                {
                    Data = data,
                    StatusCode = "0"
                };
            }
            catch(Exception)
            {
                return new ErrorStatus();
            }
        }

        [HttpDelete]
        public Status Delete(Ratio ratio)
        {
            try
            {
                _ratioRepository.Delete(ratio);
                _uow.Commit();
                return new Status
                {
                    Data = "Ratio is deleted successfully",
                    StatusCode = "0"
                };
            }
            catch(Exception)
            {
                return new ErrorStatus();
            }
            
        }
    }
}
