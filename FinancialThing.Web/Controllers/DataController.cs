using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace FinancialThing.Controllers
{
    public class DataController : Controller
    {
        private IDataGrabber _grabber;

        public DataController(IDataGrabber grabber)
        {
            _grabber = grabber;
        }
        //
        // GET: /Data/
        public ActionResult Index()
        {
            var states = Session["states"] as Dictionary<string, bool>;

            var data = JsonConvert.DeserializeObject<IEnumerable<Company>>(_grabber.Grab("http://localhost:53357/api/data/"));
            //var companies = data.Select(x => new CompanyDetails() { Id = x.Id, FullName = x.FullName, StockName = x.StockName });
            var dataViewModels = new List<DataViewModel>();
            //var companyViewModels = new List<CompanyViewModel>();
            foreach (var datum in data)
            {
                var id = datum.Id.ToString();

                dataViewModels.Add(new DataViewModel()
                {
                    CompanyViewModel = new CompanyViewModel()
                    {
                        DisplayName = datum.FullName,
                        Id = id,
                        Toggle = new Toggle()
                        {
                            Id = id,
                            State = states.ContainsKey(id) ? states[id] : false,
                        },
                        Active = "hide"
                    },
                    Financials = datum.Financials,
                });
                var active = dataViewModels.FirstOrDefault(c=>c.CompanyViewModel.Toggle.State == true);
                if (active != null)
                {
                    active.CompanyViewModel.Active = "show";
                }
            }
            return View(dataViewModels);
        }

        public ActionResult GenerateAll()
        {
            _grabber.Put("http://localhost:53357/api/data/", "");
            return null;
        }

        [HttpPost]
        public ActionResult Generate(string id)
        {
            _grabber.Put(string.Format("http://localhost:53357/api/data/{0}", id), "");
            return null;
        }
	}
}