using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;

namespace FinancialThing.Controllers
{
    public class CompanyController : Controller
    {
        private IDataGrabber _grabber;

        public CompanyController(IDataGrabber grabber)
        {
            _grabber = grabber;
        }
        //
        // GET: /Company/
        public ActionResult Index()
        {
            if(Session["states"] == null)
            {
                Session["states"] = new Dictionary<string, bool>();
            }
            var states = Session["states"] as Dictionary<string, bool>;

            var companies = JsonConvert.DeserializeObject<IEnumerable<CompanyDetails>>(_grabber.Grab("http://localhost:53357/api/company/"));

            var companyViewModels = new List<CompanyViewModel>();
            foreach (var company in companies)
            {
                var id = company.Id.ToString();
                states[id] = states.ContainsKey(id) && states[id];
                companyViewModels.Add(new CompanyViewModel()
                {
                    DisplayName = company.FullName,
                    Id = id,
                    Toggle = new Toggle()
                    {
                        Id = id,
                        State =  states[id]
                    }
                });
            }

            return View(companyViewModels);
        }

        [HttpPost]
        public void Toggle(Toggle toggle)
        {
            var states = Session["states"] as Dictionary<string, bool>;

            states[toggle.Id] = !toggle.State;

            Session["states"] = states;
        }

        public void ToggleAll(int? id)
        {
            var toggle = id != 0;
            var states = Session["states"] as Dictionary<string, bool>;
            var ids = new List<string>();
            foreach (var key in states.Keys)
            {
                ids.Add(key);
            }

            foreach (var id1 in ids)
            {
                states[id1] = toggle;
            }

            Session["states"] = states;
        }

        public ActionResult AddCompany(NewCompany company)
        {
            if (company != null)
            {
                var data = JsonConvert.SerializeObject(company);
                var str = _grabber.Post("http://localhost:53357/api/company/", data);
                var res = JsonConvert.DeserializeObject<NewCompany>(str);
                if (res.Code != "None")
                {
                    var states = Session["states"] as Dictionary<string, bool>;
                    states.Add(res.Id.ToString(), false);
                    Session["states"] = states;
                    var viewModel = new CompanyViewModel()
                    {
                        DisplayName = res.FullName,
                        Id = res.Id.ToString(),
                        Toggle = new Toggle()
                        {
                            Id = res.Id.ToString(),
                            State = false
                        }
                    };
                    return PartialView("Partial/_Company", viewModel);
                }
                return View("Partial/_Company", null);
            }
            return View("Partial/_Company", null); ;
        }
	}
}