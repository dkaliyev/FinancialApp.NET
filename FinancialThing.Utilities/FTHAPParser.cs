using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FinancialThing.Configuration;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace FinancialThing.Utilities
{
    public class FTHAPParser: IParser<Company, StockExchange>
    {
        private IDataGrabber _grabber;

        private IEnumerable<Dictionary> _dictionaries; 

        //TODO Move urls to config
        private string Url { get; set; }
        private string CompanyName { get; set; }

        string pattern = "[()]";
        private Regex _rgx;

        public FTHAPParser(IDataGrabber grabber, IRepository<Dictionary, Guid> dicRepository)
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

        public async Task<List<int>> GetYears(string url)
        {
            var data = await _grabber.Get(url);
            var html = new HtmlDocument();
            html.LoadHtml(data);
            return GetYears(html);
        }

        public List<int> GetYears(HtmlDocument doc)
        {
            var yearNodes =
               doc.DocumentNode;
            var head = yearNodes.SelectSingleNode("//thead");
            var tr = head.ChildNodes.FirstOrDefault(c => c.Name == "tr");
            var ths = tr.ChildNodes.Where(t => t.Name == "th" && ((t.Attributes["class"]!=null && t.Attributes["class"].Value!="mod-ui-table__header--text")||t.Attributes["class"] == null));
            
            List<int> years = new List<int>();
            foreach (var node in ths)
            {
                years.Add(int.Parse(node.InnerText));
            }
            return years;
        }

        private IEnumerable<HtmlNode> GetBodies(HtmlNode doc)
        {
            //var childNodes = doc.Descendants().FirstOrDefault(c => c.Name.ToLower() == "table".ToLower());
            var childNodes = doc.Descendants("table").FirstOrDefault(t => t.Attributes["data-ajax-content"] != null);
            var bodies = childNodes.ChildNodes.Where(n => n.Name == "tbody");
            return bodies;
        }

        private HtmlNode GetSection(IEnumerable<HtmlNode> bodies, string name)
        {
            var section = bodies.First(b => b.ChildNodes.Any(n => n.InnerText.ToLower().Contains(name.ToLower())));
            return section;
        }

        private IEnumerable<HtmlNode> GetRows(HtmlNode node, string name)
        {
            var assets = node.ChildNodes.Where(n => n.Name == "tr" && !n.Attributes["class"].Value.Contains("section"));
            return assets;
        }

        private IList<string> GetAssets(HtmlNode row, int yearCount)
        {
            var values = new List<string>();
            var tds = row.ChildNodes
                   .Where(d => !d.Attributes.Contains("class"));
            foreach (var node in tds)
            {
                values.Add(node.InnerText);
            }
            if (values.Count != yearCount)
            {
                throw new Exception("Number of values is not equal to number of years");
            }
            return values;
        }

        private string GetDataName(HtmlNode row)
        {
            var td = row.ChildNodes.FirstOrDefault(n => n.Attributes["class"].Value == "label");
            if (td != null)
                return td.InnerText;
            return "";
        }

        private Data GetData(HtmlNode row, string name, int ind)
        {
            return null;
        }

        public async Task<string> GetCompanyName(string name, StockExchange exch)
        {
            var companyNameHtml = await _grabber.Get(string.Format(CompanyName, name, exch.Marker));
            var nameHtmlDoc = new HtmlDocument();
            nameHtmlDoc.LoadHtml(companyNameHtml);
            var compName = GetCompanyName(nameHtmlDoc);
            return compName;
        }

        public async Task<Company> Parse(string name, StockExchange exchange)
        {
            var pages = DataMappingConfiguration.Instance.Pages;

            Company company = new Company();

            Financials financials = new Financials();

            financials.Pages = new HashSet<Page>();

            financials.TotalAssets = new HashSet<TotalAssets>();

            financials.TotalRevenue = new HashSet<TotalRevenue>();

            financials.Company = company;

            var companyNameHtml = await _grabber.Get(string.Format(CompanyName, name, exchange.Marker));
            var nameHtmlDoc = new HtmlDocument();
            nameHtmlDoc.LoadHtml(companyNameHtml);
            var compName = GetCompanyName(nameHtmlDoc);
            company.FullName = compName;
            company.Financials = financials;
            company.DateAdded = DateTime.Now;

            company.StockName = name;
            company.StockExchange = exchange;

            foreach (var page in pages)
            {
                var pageObj = new Page();
                pageObj.Financials = financials;
                pageObj.Dictionary = _dictionaries.FirstOrDefault(d => d.Code == page.Code);
                var bsHtml = await _grabber.Get(string.Format(Url, name, exchange.Marker, page.Name));
                var bsHtmlDoc = new HtmlDocument();
                bsHtmlDoc.LoadHtml(bsHtml);
                var mainBody = bsHtmlDoc.DocumentNode.Descendants("body").FirstOrDefault();
                var bodies = GetBodies(mainBody);
                if (bodies.Count() != int.Parse(page.Count))
                    return null;
                var years = GetYears(bsHtmlDoc);
                pageObj.MaxYear = years.Max();
                pageObj.MinYear = years.Min();
                pageObj.Statements = new HashSet<StatementClass>();
                foreach (var statementClass in page.Sections)
                {
                    var stClass = new StatementClass();
                    stClass.Data = new HashSet<Data>();

                    stClass.Page = pageObj;

                    stClass.Dictionary = _dictionaries.FirstOrDefault(d => d.Code == statementClass.Code);
                    var assetBody = GetSection(bodies, statementClass.DisplayName);

                    var rows = GetRows(assetBody, statementClass.DisplayName);

                    foreach (var row in rows)
                    {
                        int yearCount = years.Count;
                        var dataName = GetDataName(row).Replace("&amp;", "&");
                        var values = GetAssets(row, yearCount);
                        var data = new Data();

                        data.StatementClass = stClass;
                        data.Dictionary =
                            _dictionaries.FirstOrDefault(d => d.DisplayName.ToLower().Equals(dataName.ToLower())&&d.ParentCode.Equals(stClass.Dictionary.Code));
                        
                        data.Values = new HashSet<Value>();
                        for (int i = 0; i < yearCount; i++)
                        {
                            var value = new Value();
                            value.Data = data;
                            value.Year = years[i].ToString();

                            values[i] = _rgx.Replace(values[i], "");

                            if (values[i] == "--")
                            {
                                values[i] = "0";
                            }

                            value.DataValue = Convert.ToDecimal(values[i]);

                            data.Values.Add(value);
                            if (data.Dictionary.Code == "TR")
                            {
                                var totalRevenue = new TotalRevenue();
                                totalRevenue.Financials = financials;

                                totalRevenue.Value = value.DataValue;
                                 
                                totalRevenue.Year = int.Parse(value.Year);
                                financials.TotalRevenue.Add(totalRevenue);
                            }
                            if (data.Dictionary.Code == "TA")
                            {
                                var totalAsssets = new TotalAssets();
                                totalAsssets.Financials = financials;

                                totalAsssets.Value = value.DataValue;
                                
                                totalAsssets.Year = int.Parse(value.Year);
                                financials.TotalAssets.Add(totalAsssets);
                            }
                        }
                        stClass.Data.Add(data);
                    }
                    pageObj.Statements.Add(stClass);
                }
                financials.Pages.Add(pageObj);
            }
            return company;
        }
    }
}