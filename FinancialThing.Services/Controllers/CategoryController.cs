using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;

namespace FinancialThing.Services.Controllers
{
    public class CategoryController: ApiController
    {
        private readonly IDatabaseRepository<GroupCategory, Guid> _repo;
        private IUnitOfWork _uow;
        public CategoryController(IDatabaseRepository<GroupCategory, Guid> repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get()
        {
            var dics = _repo.GetQuery();

            return FTJsonSerializer.Serialize(dics);
        }

        public GroupCategory Get(Guid id)
        {
            return _repo.GetById(id);
        }
    }
}