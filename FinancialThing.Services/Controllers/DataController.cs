﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using System.Threading.Tasks;

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

        public Status Get()
        {
            var companies = _companyRepository.GetQuery();
            var serializedObject = FTJsonSerializer.Serialize(companies);
            return new Status
            {
                StatusCode = "0",
                Data = serializedObject
            };
        }

        public Status Get(Guid id)
        {
            var companies = _companyRepository.GetById(id);

            var serializedObject = FTJsonSerializer.Serialize(companies);
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