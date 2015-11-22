using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Utilities
{
    public class FileDownloader
    {
        public void GetFile(string url, string name)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, name);
            }
        }
    }
}
