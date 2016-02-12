using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Models;

namespace FinancialThing.Services.Controllers
{
    public class SectorController:ApiController
    {
        private IDatabaseRepository<Sector, Guid> _sectorsRepo;
        private IUnitOfWork _uow;

        public SectorController(IDatabaseRepository<Sector, Guid> sectorRepo, IUnitOfWork uow)
        {
            _sectorsRepo = sectorRepo;
            _uow = uow;
        }

        public IEnumerable<Sector> Get()
        {
            return _sectorsRepo.GetQuery();
        }

        public Sector Get(Guid id)
        {
            return _sectorsRepo.GetById(id);
        }

        public JsonResult Post(Sector sector)
        {
            var result = _sectorsRepo.Add(sector);
            _uow.Commit();
            return new JsonResult()
            {
                Data = result
            };
        }
    }
}