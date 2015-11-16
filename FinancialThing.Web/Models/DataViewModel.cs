using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialThing.Models
{
    public class DataViewModel
    {
        public CompanyViewModel CompanyViewModel { get; set; }

        public Financials Financials { get; set; }
    }
}