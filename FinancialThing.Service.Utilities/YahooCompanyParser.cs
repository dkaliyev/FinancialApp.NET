using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FinancialThing.Models;
using HtmlAgilityPack;

namespace FinancialThing.Utilities
{
    public class YahooCompanyParser: ICompanyParser<YahooCompany>
    {
        public IEnumerable<YahooCompany> Parse(string data)
        {
            var companies = new List<YahooCompany>();
            var document = new HtmlDocument();
            document.LoadHtml(data);
            var firstTable = document.DocumentNode.SelectSingleNode("//table[@class='yfnc_tableout1']");
            var secondTable = firstTable.Descendants("table").FirstOrDefault();
            var rows = secondTable.Descendants("tr");

            foreach (var row in rows)
            {
                var cols = row.Descendants("td").ToList();
                if (cols.Count()!=0)
                {
                    var sym = cols[0].InnerText;
                    var name = cols[1].InnerText;

                    var company = new YahooCompany()
                    {
                        Symbol = sym,
                        FullName = name
                    };

                    companies.Add(company);
                }
                
            }
            return companies;
        }
    }
}
