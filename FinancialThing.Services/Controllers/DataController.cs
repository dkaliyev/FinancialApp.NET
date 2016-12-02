using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using System.Threading.Tasks;
using System.Net;
using System.Text;

namespace FinancialThing.Services.Controllers
{
    public class DataController : ApiController
    {
        private IDatabaseRepository<Company, Guid> _companyRepository;
        private IParser<Company, StockExchange> _parser;
        private IDataMerger<Company> _dataMerger;
        private IUnitOfWork _uow;

        public DataController(IDatabaseRepository<Company, Guid> companyRepository,
            IParser<Company, StockExchange> parser, IUnitOfWork uow, IDataMerger<Company> dataMerger)
        {
            _companyRepository = companyRepository;
            _parser = parser;
            _uow = uow;
            _dataMerger = dataMerger;
        }

        public HttpResponseMessage Get(int page = 1)
        {
            int size = 5;
            var companies = _companyRepository.GetQuery().Take(size).Skip((page - 1) * size).ToList();
            var expandedCompanies = new List<ExpandedCompany>();

            foreach (var company in companies)
            {
                var expandedCompany = new ExpandedCompany();
                expandedCompany.Id = company.Id;
                expandedCompany.DateAdded = company.DateAdded;
                expandedCompany.FullName = company.FullName;
                expandedCompany.Industry = company.Industry;
                expandedCompany.Sector = company.Sector;
                expandedCompany.StockExchange = company.StockExchange;
                expandedCompany.StockName = company.StockName;
                expandedCompany.MaxYear = company.Financials.Pages.First().MaxYear;
                expandedCompany.MinYear = company.Financials.Pages.First().MinYear;
                //expandedCompany.TotalAssets = company.Financials.TotalAssets;
                //expandedCompany.TotalRevenue = company.Financials.TotalRevenue;
                expandedCompany.Data = new List<ShortData>();
                foreach (var financial in company.Financials.Pages)
                {
                    foreach (var statement in financial.Statements)
                    {
                        var data = statement.Data.Select(d => new ShortData
                        {
                            Values = d.Values.Select(x => new ShortValue { DataValue = x.DataValue, Year = x.Year }).ToList(),
                            Dictionary = d.Dictionary
                        }).ToList();
                        expandedCompany.Data.AddRange(data);
                    }
                }

                expandedCompanies.Add(expandedCompany);
            }
            var response = new Response<IEnumerable<ExpandedCompany>>
            {
                Data = expandedCompanies,
                Status = 0,
                Message = ""
            };
            var serializedObject = FTJsonSerializer<Response<IEnumerable<ExpandedCompany>>>.Serialize(response);

            string yourJson = serializedObject;
            var result = this.Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StringContent(yourJson, Encoding.UTF8, "application/json");
            return result;
        }

            //return serializedObject;        }

        public Status Get(Guid id)
        {
            var company = _companyRepository.GetById(id);

            var serializedObject = FTJsonSerializer<Company>.Serialize(company);
            return new Status
            {
                StatusCode = "0",
                Data = serializedObject
            };
        }

        public async Task<Status> PutAll()
        {
            var companies = _companyRepository.GetQuery();
            try
            {
                foreach (var oldCompany in companies)
                {
                    if (oldCompany != null)
                    {
                        var newCompany = await _parser.Parse(oldCompany.StockName, oldCompany.StockExchange);
                        _dataMerger.Merge(oldCompany, newCompany);
                        _companyRepository.Update(oldCompany);
                    }
                }
                _uow.Commit();
                return new Status
                {
                    StatusCode = "0",
                    Data = "Data generated successfully for all companies"
                };
            }
            catch (Exception)
            {
                return new Status
                {
                    StatusCode = "1",
                    Data = "There were issues generating the data, please try again later"
                };
            }

        }

        public async Task<Status> Put(Guid id)
        {
            var oldCompany = _companyRepository.GetById(id);
            try
            { 
                if (oldCompany != null)
                {
                    var newCompany = await _parser.Parse(oldCompany.StockName, oldCompany.StockExchange);
                    if (newCompany == null)
                        return new Status
                        {
                            StatusCode = "1",
                            Data = "Could not parse data, please try again later"
                        };
                    _dataMerger.Merge(oldCompany, newCompany);
                    _companyRepository.Update(oldCompany);
                    _uow.Commit();
                    return new Status
                    {
                        StatusCode = "0",
                        Data = "Data has been generated successfully"
                    };
                }
                return new Status
                {
                    StatusCode = "1",
                    Data = "Company does not exist"
                };
            }

            catch(Exception)
            {
                return new Status
                {
                    StatusCode = "1",
                    Data = "There was an issue generating the data"
                };
            }
        }
    }
}