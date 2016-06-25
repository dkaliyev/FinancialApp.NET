using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.DTO;
using FinancialThing.Models;
using FinancialThing.Utilities;

namespace FinancialThing.Services.Controllers
{
    public class IndustryController : ApiController
    {
        private IDatabaseRepository<Industry, Guid> _indRepo;
        private IDatabaseRepository<Sector, Guid> _sectorRepo;
        private IUnitOfWork _uow;

        public IndustryController(IDatabaseRepository<Industry, Guid> indRepo, IDatabaseRepository<Sector, Guid> sectorRepo, IUnitOfWork uow)
        {
            _indRepo = indRepo;
            _sectorRepo = sectorRepo;
            _uow = uow;
        }


        public Status Get()
        {
            try
            {
                var data = _indRepo.GetQuery();
                return new Status
                {
                    Data = FTJsonSerializer.Serialize(data),
                    StatusCode = "0"
                };
            }
            catch
            {
                return new ErrorStatus();
            }
        }

        public Status Get(Guid id)
        {
            try
            {
                return new Status
                {
                    Data = FTJsonSerializer.Serialize(_indRepo.GetById(id)),
                    StatusCode = "0"
                };
            }
            catch (Exception)
            {
                return new ErrorStatus();
            }
        }

        public Status Post(Industry industry)
        {
            try
            {
                var sector = _sectorRepo.GetById(industry.Sector.Id);
                var result = _indRepo.Add(new Industry() { Code = industry.Code, DisplayName = industry.DisplayName, Sector = sector });
                _uow.Commit();
                return new Status
                {
                    Data = FTJsonSerializer.Serialize(result),
                    StatusCode = "0"
                };
            }
            catch (Exception)
            {
                return new ErrorStatus();
            }
        }
    }
}
