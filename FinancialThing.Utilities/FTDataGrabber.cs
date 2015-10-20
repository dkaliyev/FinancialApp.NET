using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FinancialThing.Models;

namespace FinancialThing.Utilities
{
    public class FTDataGrabber: IDataGrabber
    {
        public string Grab(string url)
        {
            string htmlCode;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url);
            }
            return htmlCode;
        }
    }
}
