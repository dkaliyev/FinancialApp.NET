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
    public class NewFTHAPParser : IParser<Company, StockExchange>
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
            CompanyName = "http://markets.ft.com/data/equities/tearsheet/financials?s={0}:{1}";
        }

        private string GetCompanyName(HtmlDocument doc)
        {
            var name = doc.DocumentNode.SelectSingleNode("//h1[@class='mod-tearsheet-overview__header__name mod-tearsheet-overview__header__name--large']").InnerText;
            return name;
        }

        private List<int> GetYears(HtmlDocument doc)
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

        public async Task<List<int>> GetYears(string url)
        {
            var data = await _grabber.Get(url);
            var html = new HtmlDocument();
            html.LoadHtml(data);
            return GetYears(html);
        }

        private HtmlNodeCollection GetRows(HtmlDocument doc)
        {
            var table = doc.DocumentNode.SelectSingleNode("//table[@class='mod-ui-table']");
            var thead = table.SelectSingleNode("//thead");
            thead.Remove();
            var rows = table.ChildNodes;
            return rows;
        }

        public async Task<HtmlNodeCollection> GetRows(string url)
        {
            var data = await _grabber.Get(url);
            var html = new HtmlDocument();
            html.LoadHtml(data);
            return GetRows(html);
        }

        public IEnumerable<HtmlNode> GetItems(HtmlNodeCollection rows)
        {
            var result = rows.Where(row => (row.Attributes["class"] != null && (row.Attributes["class"].Value == "mod-ui-table__row--striped" || row.Attributes["class"].Value == "mod-ui-table__row--highlight"
            || row.Attributes["class"].Value == "mod-ui-table__row--highlight mod-ui-table__row--striped"))
                    || row.Attributes["class"] == null);

            return result;
        }

        public ISet<StatementClass> GetData(IEnumerable<HtmlNode> items, List<int> years, Page page)
        {
            //var data = new HashSet<Data>();

            var statements = new HashSet<StatementClass>();

            var statementsDic = new List<string>();

            foreach (var item in items)
            {
                var datum = new Data();
                var name = item.FirstChild.InnerText.Replace("&amp;", "&").Replace("&#39;", "'");
                var values = new HashSet<Value>();
                var vals = item.ChildNodes.Where(n=>n.Name == "td").ToList();
                years = years.OrderByDescending(x => x).ToList();
                var header = GetHeader(item);
                var statementClass = new StatementClass();
                var dic = _dictionaries.FirstOrDefault(x => x.SiteName.ToLower() == header.FirstChild.InnerText.ToLower());
                if(statementsDic.Contains(dic.Code))
                {
                    statementClass = statements.Single(x => x.Dictionary.Code == dic.Code);
                }
                else
                {
                    statementClass.Dictionary = dic;
                    statementClass.Data = new HashSet<Data>();
                    statements.Add(statementClass);
                    statementsDic.Add(dic.Code);
                }

                datum.Dictionary = _dictionaries.SingleOrDefault(x => x.SiteName == name && x.ParentCode == statementClass.Dictionary.Code);

                for (int i = 0; i < years.Count(); i++)
                {
                    var val = vals[i].InnerText == "--" ? 0 : Convert.ToDecimal(vals[i].InnerText.Replace("(", "").Replace(")", ""));
                    var year = years[i].ToString();
                    values.Add(new Value
                    {
                        DataValue = val,
                        Year = year,
                        Data = datum
                    });
                    if (datum.Dictionary.Code == "TR")
                    {
                        var totalRevenue = new TotalRevenue();
                        totalRevenue.Financials = page.Financials;

                        totalRevenue.Value = val;

                        totalRevenue.Year = int.Parse(year);
                        page.Financials.TotalRevenue.Add(totalRevenue);
                    }
                    if (datum.Dictionary.Code == "TA")
                    {
                        var totalAsssets = new TotalAssets();
                        totalAsssets.Financials = page.Financials;

                        totalAsssets.Value = val;

                        totalAsssets.Year = int.Parse(year);
                        page.Financials.TotalAssets.Add(totalAsssets);
                    }
                }
                
                datum.Values = values;
                datum.StatementClass = statementClass;
                statementClass.Data.Add(datum);
                //data.Add(datum);
                //statementClass.Data = data;
            }
            return statements;
        }

        public HtmlNode GetHeader(HtmlNode item)
        {
            do
            {
                item = item.PreviousSibling;
            }
            while (!(item.Attributes["class"] != null && item.Attributes["class"].Value == "mod-ui-table__row--section-header"));
            return item;
        }

        public async Task<Company> Parse(string name, StockExchange exchange)
        {
            var url = string.Format(CompanyName, name, exchange.Marker);
            var companyNameHtml = await _grabber.Get(url);
            var nameHtmlDoc = new HtmlDocument();
            nameHtmlDoc.LoadHtml(companyNameHtml);

            var company = new Company();

            var financials = new Financials();

            financials.Pages = new HashSet<Page>();

            financials.TotalAssets = new HashSet<TotalAssets>();

            financials.TotalRevenue = new HashSet<TotalRevenue>();

            var compName = GetCompanyName(nameHtmlDoc);
            company.FullName = compName;
            company.Financials = financials;
            company.DateAdded = DateTime.Now;

            company.StockName = name;
            company.StockExchange = exchange;

            financials.Company = company;

            var pages = new List<string> { "BalanceSheet", "CashFlow", "IncomeStatement" };

            foreach(var page in pages)
            {
                var pageObj = new Page();
                pageObj.Financials = financials;
                pageObj.Dictionary = _dictionaries.FirstOrDefault(d => d.Code == page);
                var bsHtml = await _grabber.Get(string.Format(Url, name, exchange.Marker, page));
                var bsHtmlDoc = new HtmlDocument();
                bsHtmlDoc.LoadHtml(bsHtml);

                var years = GetYears(bsHtmlDoc);
                pageObj.MaxYear = years.Max();
                pageObj.MinYear = years.Min();

                var rows = GetRows(bsHtmlDoc);
                var items = GetItems(rows);

                pageObj.Statements = GetData(items, years, pageObj);

                financials.Pages.Add(pageObj);
            }
            return company;
        }

        public async Task<string> GetCompanyName(string name, StockExchange exchange)
        {
            var companyNameHtml = await _grabber.Get(string.Format(CompanyName, name, exchange.Marker));
            var nameHtmlDoc = new HtmlDocument();
            nameHtmlDoc.LoadHtml(companyNameHtml);
            var compName = GetCompanyName(nameHtmlDoc);
            return compName;
        }
    }
}
