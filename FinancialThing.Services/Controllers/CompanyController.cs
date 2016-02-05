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
        private IDatabaseRepository<Company, Guid> _companyRepository;
        private IParser<Company, StockExchange> _parser;
        private IUnitOfWork _uow;

        public CompanyController(IDatabaseRepository<Company, Guid> companyRepository,
            IParser<Company, StockExchange> parser, IUnitOfWork uow)
        {
            _companyRepository = companyRepository;
            _parser = parser;
            _uow = uow;
        }
        public HttpResponseMessage GetAll()
        {
            var companies = _companyRepository.GetQuery().Select(x => new Company() { Id = x.Id, FullName = x.FullName, StockName = x.StockName, StockExchange = x.StockExchange, Industry = x.Industry, Sector = x.Sector});

            return FTJsonSerializer.Serialize(companies);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var company = _companyRepository.GetQuery().Where(c => c.Id == id).Select(x => new Company() { Id = x.Id, FullName = x.FullName, StockName = x.StockName, StockExchange = x.StockExchange, Industry = x.Industry, Sector = x.Sector });
            return FTJsonSerializer.Serialize(company);
        }

        [HttpPost]
        public HttpResponseMessage Post(Company newCompany)
        {
            var company = _companyRepository.FindBy(c => c.StockName == newCompany.StockName);
            if (company != null)
            {
                return FTJsonSerializer.Serialize(new CompanyDetails() { Code = "None" });
            }
            

            var compFullName = _parser.GetCompanyName(newCompany.StockName, newCompany.StockExchange);
            if (compFullName != null)
            {
                newCompany.FullName = compFullName;
                newCompany.DateAdded = DateTime.Today;
                var id = _companyRepository.Add(newCompany);
                _uow.Commit();
                //newCompany.FullName = compFullName;
                newCompany.Id = id.Id;
                return FTJsonSerializer.Serialize(newCompany);
            }
            return FTJsonSerializer.Serialize(new CompanyDetails() { Code = "None" });
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
