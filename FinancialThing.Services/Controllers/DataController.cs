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
    public class DataController: ApiController
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

        public HttpResponseMessage Get()
        {
            var companies = _companyRepository.GetQuery();

            return FTJsonSerializer.Serialize(companies);
        }

        public HttpResponseMessage Get(Guid id)
        {
            var companies = _companyRepository.GetById(id);

            return FTJsonSerializer.Serialize(companies);
        }

        public HttpResponseMessage PutAll()
        {
            var companies = _companyRepository.GetQuery();
            foreach (var oldCompany in companies)
            {
                if (oldCompany != null)
                {
                    var newCompany = _parser.Parse(oldCompany.StockName, oldCompany.StockExchange);
                    _dataMerger.Merge(oldCompany, newCompany);
                    _companyRepository.Update(oldCompany);
                }
            }
            _uow.Commit();
            return new HttpResponseMessage();
        }

        public HttpResponseMessage Put(Guid id)
        {
            var oldCompany = _companyRepository.GetById(id);
            if (oldCompany != null)
            {
                var newCompany = _parser.Parse(oldCompany.StockName, oldCompany.StockExchange);
                if(newCompany==null)
                    return new HttpResponseMessage();
                _dataMerger.Merge(oldCompany, newCompany);
                _companyRepository.Update(oldCompany);
                _uow.Commit();
                return new HttpResponseMessage();
            }
            return new HttpResponseMessage();
        }
    }
}