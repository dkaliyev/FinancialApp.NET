using System.Linq;
using System.Net;
using System.Text;

namespace FinancialThing.Utilities
{
    public class FTDataGrabber: IDataGrabber
    {
        public string Grab(string url)
        {
            string htmlCode;
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                htmlCode = client.DownloadString(url);
            }
            return htmlCode;
        }

        public void Put(string url, string data)
        {
            using (var client = new System.Net.WebClient())
            {
                client.UploadData(url, "PUT", Encoding.ASCII.GetBytes(data));
            }
        }

        public string Post(string url, string data)
        {
            using (var client = new System.Net.WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                return Encoding.ASCII.GetString(client.UploadData(url, "POST", Encoding.ASCII.GetBytes(data??"")));
            }
        }
    }
}
