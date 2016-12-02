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
using System.Threading.Tasks;

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
        public Status GetAll()
        {
            try
            {
                var companies = _companyRepository.GetQuery().Select(x => new Company() { Id = x.Id, FullName = x.FullName, StockName = x.StockName, StockExchange = x.StockExchange, Industry = x.Industry, Sector = x.Sector });

                return new Status
                {
                    Data = FTJsonSerializer<IQueryable<Company>>.Serialize(companies),
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
                var company = _companyRepository.GetQuery().Where(c => c.Id == id).Select(x => new Company() { Id = x.Id, FullName = x.FullName, StockName = x.StockName, StockExchange = x.StockExchange, Industry = x.Industry, Sector = x.Sector });
                return new Status
                {
                    Data = FTJsonSerializer<IQueryable<Company>>.Serialize(company),
                    StatusCode = "0"
                };
            }
            catch (Exception)
            {
                return new ErrorStatus();
            }
        }

        [HttpPost]
        public async Task<Status> Post(Company newCompany)
        {
            try
            {
                var company = _companyRepository.FindBy(c => c.StockName == newCompany.StockName);
                if (company != null)
                {
                    return new Status
                    {
                        Data = "Company already exists",
                        StatusCode = "1"
                    };
                }


                var compFullName = await _parser.GetCompanyName(newCompany.StockName, newCompany.StockExchange);
                if (compFullName != null)
                {
                    newCompany.FullName = compFullName;
                    newCompany.DateAdded = DateTime.Today;
                    var id = _companyRepository.Add(newCompany);
                    _uow.Commit();
                    //newCompany.FullName = compFullName;
                    newCompany.Id = id.Id;
                    return new Status
                    {
                        Data = FTJsonSerializer<Company>.Serialize(newCompany),
                        StatusCode = "0"
                    };
                }
                else
                {
                    return new ErrorStatus();
                }
            }
            catch (Exception)
            {
                return new ErrorStatus();
            }

        }


        public Status Delete(Guid id)
        {
            var company = _companyRepository.GetById(id);
            try
            {
                if (company != null)
                {
                    _companyRepository.Delete(company);
                    _uow.Commit();
                    return new Status
                    {
                        Data = "Company is deleted successfully",
                        StatusCode = "0"
                    };
                }
                else
                {
                    return new Status
                    {
                        Data = "Company not found",
                        StatusCode = "1"
                    };
                }
            }
            catch(Exception)
            {
                return new ErrorStatus();
            }

        }
    }
}
