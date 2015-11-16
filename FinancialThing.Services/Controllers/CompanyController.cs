using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;
using NHibernate.Exceptions;
using NHibernate.Linq.Functions;

namespace FinancialThing.Services.Controllers
{
    public class CompanyController : ApiController
    {
        private IRepository<Company, Guid> _companyRepository;
        private IParser<Company, StockExchange> _parser;
        private IRepository<StockExchange, Guid> _sexRepository;
        private IRepository<Dictionary, Guid> _dictionaryRepository;
        private IDataMerger<Company> _dataMerger; 
        private IUnitOfWork _uow;

        public CompanyController(IRepository<StockExchange, Guid> sexRepo, IRepository<Company, Guid> companyRepository, IRepository<Dictionary, Guid> dictionaryRepository, IParser<Company, StockExchange> parser, IUnitOfWork uow,
            IDataMerger<Company> dataMerger)
        {
            _companyRepository = companyRepository;
            _sexRepository = sexRepo;
            _parser = parser;
            _uow = uow;
            _dictionaryRepository = dictionaryRepository;
            _dataMerger = dataMerger;
        }
        public HttpResponseMessage GetAll()
        {
            var companies = _companyRepository.GetQuery().Select(x => new CompanyDetails() { Id = x.Id, FullName = x.FullName, StockName = x.StockName});

            return FTJsonSerializer.Serialize(companies);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var company = _companyRepository.GetQuery().Where(c=>c.Id == id).Select(x => new { FullName = x.FullName, StockName = x.StockName });
            return FTJsonSerializer.Serialize(company);
        }

        [HttpPost]
        public HttpResponseMessage Post(NewCompany newCompany)
        {
            var sex = _sexRepository.GetQuery().FirstOrDefault(x => x.Marker == newCompany.StockName);
            if (sex == null)
            {
                sex = new StockExchange()
                {
                    Marker = newCompany.StockName,
                    DateAdded = DateTime.Now
                };
                _sexRepository.Add(sex);
            }
            var company = _companyRepository.FindBy(c => c.StockName == newCompany.Code);
            if (company != null)
            {
                return FTJsonSerializer.Serialize(new NewCompany(){Code = "None"});
            }
            company = new Company()
            {
                StockName = newCompany.Code,
                StockExchange = sex,
                DateAdded = DateTime.Now
            };

            var compFullName = _parser.GetCompanyName(newCompany.Code, sex);
            if (compFullName != null)
            {
                company.FullName = compFullName;
                var id = _companyRepository.Add(company);
                _uow.Commit();
                newCompany.FullName = compFullName;
                newCompany.Id = id;
                return FTJsonSerializer.Serialize(newCompany);
            }
            return FTJsonSerializer.Serialize(new NewCompany(){Code = "None"});
        }


        public HttpResponseMessage Delete(Guid id)
        {
            var company = _companyRepository.GetById(id);
            if (company != null)
            {
                _companyRepository.Delete(company);
                _uow.Commit();
                return FTJsonSerializer.Serialize(new { Status="success" }); 
            }
            else
            {
                return FTJsonSerializer.Serialize(new { Status = "Company not found" });
            }
        }
    }
}
