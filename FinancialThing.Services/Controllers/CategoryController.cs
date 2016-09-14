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
        public CategoryController(IDatabaseRepository<GroupCategory, Guid> repo)
        {
            _repo = repo;
        }

        public Status Get()
        {
            try
            {
                var dics = _repo.GetQuery();
                var data = FTJsonSerializer.Serialize(dics);

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

        public GroupCategory Get(Guid id)
        {
            return _repo.GetById(id);
        }
    }
}