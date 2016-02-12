using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.Models;

namespace FinancialThing.Services.Controllers
{
    public class CategoryEntryController : ApiController
    {
        private readonly IDatabaseRepository<CategoryEntry, Guid> _repo;
        private IUnitOfWork _uow;
        public CategoryEntryController(IDatabaseRepository<CategoryEntry, Guid> repo)
        {
            _repo = repo;
        }

        public IEnumerable<CategoryEntry> Get()
        {
            var dics = _repo.GetQuery();

            return dics;
        }

        public CategoryEntry Get(Guid id)
        {
            return _repo.GetById(id);
        }
    }

    
}