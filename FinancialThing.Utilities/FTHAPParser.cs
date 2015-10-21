using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FinancialThing.DataAccess.nHibernate;
using FinancialThing.Models;
using FinancialThing.Services.Utilities;
using HtmlAgilityPack;

namespace FinancialThing.Utilities
{
    public class FTHAPParser: IParser<Company>
    {
        private IDataGrabber _grabber;
        private IEnumerable<Dictionary> _dictionary;

        //TODO Move urls to config
        private string Url { get; set; }
        private string CompanyName { get; set; }


        public FTHAPParser(IDataGrabber grabber, IEnumerable<Dictionary> dictionary)
        {
            _dictionary = dictionary;
            _grabber = grabber;
            Url = "http://markets.ft.com/research/Markets/Tearsheets/Financials?s={0}:{1}&subview={2}";
            CompanyName = "http://markets.ft.com/research/Markets/Tearsheets/Financials?s={0}:{1}";
        }

        public Company Parse(string name, string exchange)
        {
            var pages = DataMappingConfiguration.Instance.Pages;

            Company company = new Company();

            Financials financials = new Financials();

            financials.Pages = new HashSet<Page>();

            var companyNameHtml = _grabber.Grab(string.Format(CompanyName, name, exchange));
            var nameHtmlDoc = new HtmlDocument();
            nameHtmlDoc.LoadHtml(companyNameHtml);
            var compName = GetCompanyName(nameHtmlDoc);
            company.FullName = compName;
            company.Financials = financials;
            company.DateAdded = DateTime.Now;
            company.StockName = exchange;

            foreach (var page in pages)
            {
                var pageObj = new Page();
                pageObj.Dictionary = _dictionary.FirstOrDefault(d => d.Code == page.Code);
                var bsHtml = _grabber.Grab(string.Format(Url, name, exchange, page.Name));
                var bsHtmlDoc = new HtmlDocument();
                bsHtmlDoc.LoadHtml(bsHtml);
                var mainBody = bsHtmlDoc.DocumentNode.Descendants("body").FirstOrDefault();
                var bodies = GetBodies(mainBody);

                var years = GetYears(bsHtmlDoc);
                pageObj.Statements = new HashSet<StatementClass>();
                foreach (var statementClass in page.Sections)
                {
                    var stClass = new StatementClass();
                    stClass.Data = new HashSet<Data>();

                    stClass.Dictionary = _dictionary.FirstOrDefault(d => d.Code == statementClass.Code);
                    var assetBody = GetSection(bodies, statementClass.DisplayName);

                    var rows = GetRows(assetBody, statementClass.DisplayName);

                    foreach (var row in rows)
                    {
                        int yearCount = years.Count;
                        var dataName = GetDataName(row);
                        var values = GetAssets(row, yearCount);
                        var data = new Data();
                        data.Dictionary =
                            _dictionary.FirstOrDefault(d => d.DisplayName.ToLower().Equals(dataName.ToLower()));
                        data.Values = new HashSet<Value>();
                        for (int i = 0; i < yearCount; i++)
                        {
                            var value = new Value();
                            value.Year = years[i].ToString();
                            value.DataValue = values[i];
                            data.Values.Add(value);
                        }
                        stClass.Data.Add(data);
                    }
                    pageObj.Statements.Add(stClass);
                }
                financials.Pages.Add(pageObj);
            }


            

            

            

            

            
            

            //var assets = 
            return company;
        }

        private string GetCompanyName(HtmlDocument doc)
        {
            var name = doc.DocumentNode.SelectSingleNode("//h1[@class='contains']")
                    .SelectSingleNode("//span[@class='formatIssueName']")
                    .InnerText;
            return name;
        }

        private List<int> GetYears(HtmlDocument doc)
        {
            var yearNodes =
               doc.DocumentNode;
            var head = yearNodes.SelectSingleNode("//thead");
            var tr = head.ChildNodes.FirstOrDefault(c => c.Name == "tr");
            var tds = tr.ChildNodes
                   .Where(d => !d.Attributes.Contains("class"));
            List<int> years = new List<int>();
            foreach (var node in tds)
            {
                years.Add(int.Parse(node.InnerText));
            }
            return years;
        }

        private IEnumerable<HtmlNode> GetBodies(HtmlNode doc)
        {
            //var childNodes = doc.Descendants().FirstOrDefault(c => c.Name.ToLower() == "table".ToLower());
            var childNodes = doc.Descendants("table").FirstOrDefault();
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
            var assets = node.ChildNodes.Where(n => n.Name == "tr" && !n.InnerText.ToLower().Contains(name.ToLower()));
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
    }
}