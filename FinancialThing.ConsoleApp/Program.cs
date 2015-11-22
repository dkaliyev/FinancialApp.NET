using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialThing.Utilities;

namespace FinancialThing.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileDownloader = new FileDownloader();
            fileDownloader.GetFile("http://real-chart.finance.yahoo.com/table.csv?s=FB&a=06&b=19&c=1990&d=10&e=22&f=2015&g=d&ignore=.csv", "~/Data/File1.csv");
        }
    }
}
