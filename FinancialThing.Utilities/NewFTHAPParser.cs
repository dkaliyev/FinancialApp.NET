using FinancialThing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using FinancialThing.DataAccess;
using HtmlAgilityPack;

namespace FinancialThing.Utilities
{
    public class NewFTHAPParser: IParser<Company, StockExchange>
    {
        private IDataGrabber _grabber;

        private IEnumerable<Dictionary> _dictionaries;

        //TODO Move urls to config
        private string Url { get; set; }
        private string CompanyName { get; set; }

        string pattern = "[()]";
        private Regex _rgx;

        public NewFTHAPParser(IDataGrabber grabber, IRepository<Dictionary, Guid> dicRepository)
        {
            _rgx = new Regex(pattern);
            _grabber = grabber;
            _dictionaries = dicRepository.GetQuery();

            Url = "http://markets.ft.com/data/equities/tearsheet/financials?s={0}:{1}&subview={2}";
            CompanyName = "http://markets.ft.com/data/equities/tearsheets/financials?s={0}:{1}";
        }

        private string GetCompanyName(HtmlDocument doc)
        {
            var name = doc.DocumentNode.SelectSingleNode("//h1[@class='mod-tearsheet-overview__header__name mod-tearsheet-overview__header__name--large']").InnerText;
            return name;
        }

        public List<int> GetYears(HtmlDocument doc)
        {
            var yearNodes =
               doc.DocumentNode;
            var head = yearNodes.SelectSingleNode("//thead");
            var tr = head.ChildNodes.FirstOrDefault(c => c.Name == "tr");
            var ths = tr.ChildNodes.Where(t => t.Name == "th" && ((t.Attributes["class"] != null && t.Attributes["class"].Value != "mod-ui-table__header--text") || t.Attributes["class"] == null));

            List<int> years = new List<int>();
            foreach (var node in ths)
            {
                years.Add(int.Parse(node.InnerText));
            }
            return years;
        }

        public ISet<StatementClass> GetStatementClasses(HtmlNodeCollection rows)
        {
            var data = new HashSet<Data>();
            foreach(var row in rows)
            {
                if(row.Attributes["class"]!=null && row.Attributes["class"].Value == "mod-ui-table__row--section-header")
                {

                }
            }
             = rows.Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "");
        }

        public Task<Company> Parse(string name, StockExchange exchange)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetCompanyName(string name, StockExchange exchange)
        {
            var companyNameHtml = await _grabber.Get(string.Format(CompanyName, name, exch.Marker));
            var nameHtmlDoc = new HtmlDocument();
            nameHtmlDoc.LoadHtml(companyNameHtml);
            var compName = GetCompanyName(nameHtmlDoc);
            return compName;
        }
    }
}
