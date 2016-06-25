using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;

namespace FinancialThing.Services.Controllers
{
    public class SectorController : ApiController
    {
        private IDatabaseRepository<Sector, Guid> _sectorsRepo;
        private IUnitOfWork _uow;

        public SectorController(IDatabaseRepository<Sector, Guid> sectorRepo, IUnitOfWork uow)
        {
            _sectorsRepo = sectorRepo;
            _uow = uow;
        }

        public Status Get()
        {
            try
            {
                return new Status
                {
                    Data = FTJsonSerializer.Serialize(_sectorsRepo.GetQuery()),
                    StatusCode = "0"
                };
            }
            catch (Exception)
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
                    Data = FTJsonSerializer.Serialize(_sectorsRepo.GetById(id)),
                    StatusCode = "0"
                };
            }
            catch(Exception)
            {
                return new ErrorStatus();
            }

        }

        public Status Post(Sector sector)
        {
            try
            {
                var result = _sectorsRepo.Add(sector);
                _uow.Commit();
                return new Status()
                {
                    Data = FTJsonSerializer.Serialize(result),
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