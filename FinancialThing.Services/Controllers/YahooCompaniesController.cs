﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;

namespace FinancialThing.Services.Controllers
{
    public class YahooCompaniesController : ApiController
    {
        private IDatabaseRepository<YahooCompany, Guid> _companyRepository;
        private IUnitOfWork _uow;
        private IDataGrabber _grabber;
        private ICompanyParser<YahooCompany> _parser;
        
        

        public YahooCompaniesController(IDatabaseRepository<YahooCompany, Guid> companyRepository, IUnitOfWork uow, ICompanyParser<YahooCompany> parser, IDataGrabber grabber)
        {
            _companyRepository = companyRepository;
            _uow = uow;
            _grabber = grabber;
            _parser = parser;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            var companies = _companyRepository.GetQuery().ToList();
            return FTJsonSerializer.Serialize(companies);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post()
        {
            List<int> indexes = new List<int>() { 0, 1, 2 };
            string link = "https://uk.finance.yahoo.com/q/cp?s={0}&c={1}";
            var list = "^FTSE";
            foreach (var ind in indexes)
            {
                var l = string.Format(link, list, ind);
                var data = _grabber.Grab(l);
                var res = _parser.Parse(data);
                foreach (var yahooCompany in res)
                {
                    if (_companyRepository.GetQuery().FirstOrDefault(c => c.Symbol == yahooCompany.Symbol) == null)
                    {
                        _companyRepository.SaveOrUpdate(yahooCompany);
                    }
                }
            }
            _uow.Commit();
            return FTJsonSerializer.Serialize(new {status = "success"});
        }
    }
}
