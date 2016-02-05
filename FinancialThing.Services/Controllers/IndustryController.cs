using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.DTO;
using FinancialThing.Models;

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


        public IEnumerable<Industry> Get()
        {
            return _indRepo.GetQuery();
        }

        public Industry Get(Guid id)
        {
            return _indRepo.GetById(id);
        }

        public Industry Post(Industry industry)
        {
            var sector = _sectorRepo.GetById(industry.Sector.Id);
            var result = _indRepo.Add(new Industry() {Code = industry.Code, DisplayName = industry.DisplayName, Sector = sector});
            _uow.Commit();
            return result;
        }
    }
}
