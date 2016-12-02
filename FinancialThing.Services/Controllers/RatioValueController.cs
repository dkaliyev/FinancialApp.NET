using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;

namespace FinancialThing.Services.Controllers
{
    public class RatioValueController : ApiController
    {
        private readonly IDatabaseRepository<RatioValue, Guid> _ratioValueRepo;


        private readonly IDatabaseRepository<Ratio, Guid> _ratioServiceRepository;

        private readonly IDatabaseRepository<Company, Guid> _companyServiceRepository;

        private readonly IUnitOfWork _uow;

        public RatioValueController(IDatabaseRepository<RatioValue, Guid> ratioValueRepo, IDatabaseRepository<Ratio, Guid> ratioRepo, IDatabaseRepository<Company, Guid> companyRepo, IUnitOfWork uow)
        {
            _ratioServiceRepository = ratioRepo;
            _companyServiceRepository = companyRepo;
            _ratioValueRepo = ratioValueRepo;
            _uow = uow;
        }


        public Status Get()
        {
            try
            {
                var ratios = _ratioValueRepo.GetQuery();
                var data = FTJsonSerializer<IQueryable<RatioValue>>.Serialize(ratios);
                return new Status
                {
                    StatusCode = "0",
                    Data = data
                };
            }
            catch (Exception)
            {
                return new Status
                {
                    StatusCode = "1",
                    Data = "There was an error"
                };
            }
             
        }

        public Status Post()
        {
            var companies = _companyServiceRepository.GetQuery();
            var ratios = _ratioServiceRepository.GetQuery();
            var evaluator = new RatioEvaluator(null);
            var ratioValues = _ratioValueRepo.GetQuery();

            foreach (var company in companies)
            {
                var data = company.Financials.Pages.SelectMany(p => p.Statements.SelectMany(s => s.Data)).ToList();
                foreach (var ratio in ratios)
                {
                    var value = ratioValues.FirstOrDefault(r => r.CompanyId == company.Id.ToString() && r.RatioId == ratio.Id.ToString()) ?? new RatioValue()
                    {
                        CompanyId = company.Id.ToString(),
                        RatioId = ratio.Id.ToString()
                    };

                    //value.Year = company.Financials.MaxYear.ToString();

                    try
                    {
                        var result = evaluator.Evaluate(data, ratio);

                        value.Value = result.ToString();
                    }
                    catch (InvalidOperationException)
                    {
                        value.Value = "N/A";
                    }
                    finally
                    {
                        try
                        {
                            _ratioValueRepo.SaveOrUpdate(value);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.Write(ex.Message);
                        }
                    }
                }
            }
            try
            {
                _uow.Commit();
                return new Status
                {
                    Data = "Successfully saved new ratio values",
                    StatusCode = "0"
                };
            }
            catch(Exception)
            {
                return new Status
                {
                    Data = "Unable to save new ratio values",
                    StatusCode = "1"
                };
            }
            
        }
    }
}