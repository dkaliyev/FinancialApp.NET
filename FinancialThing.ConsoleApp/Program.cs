using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialThing.Utilities;
using FinancialThing.Models;

namespace FinancialThing.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var _grabber = new FTDataGrabber();
            List<int> indexes = new List<int>() {0, 1, 2};
            string link = "https://uk.finance.yahoo.com/q/cp?s={0}&c={1}";
            var list = "^FTSE";
            foreach (var ind in indexes)
            {
                var l = string.Format(link, list, ind);
                var data = _grabber.Grab(l);
                YahooCompanyParser parser = new YahooCompanyParser();
                var res = parser.Parse(data);
            }
            
            //var grabber = new FTDataGrabber();
            //string result =
            //    grabber.Grab(
            //        "http://markets.ft.com/research/Markets/Tearsheets/Financials?s=IMT:LSE&subview=BalanceSheet");
            //Console.WriteLine(result);
            //Console.Read();
        }
    }
}
