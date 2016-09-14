using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialThing.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FinancialThing.Utilities
{
    public class AsyncHttpClient: IDataGrabber
    {
        public async Task<string> Delete(string url, string data)
        {
            string results = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(new Uri(url));
                Byte[] downloadedBytes = await response.Content.ReadAsByteArrayAsync();
                Encoding encoding = new ASCIIEncoding();
                results = encoding.GetString(downloadedBytes);
                return results;
            }
        }

        public async Task<string> Get(string url)
        {
            string results = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync(new Uri(url));
                Byte[] downloadedBytes = await response.Content.ReadAsByteArrayAsync();
                Encoding encoding = new ASCIIEncoding();
                results = encoding.GetString(downloadedBytes);
                return results;
            }
        }

        public async Task<string> Post(string url, string data)
        {
            string results = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent queryString = new StringContent(data);
                var response = await httpClient.PostAsync(new Uri(url), queryString);
                Byte[] downloadedBytes = await response.Content.ReadAsByteArrayAsync();
                Encoding encoding = new ASCIIEncoding();
                results = encoding.GetString(downloadedBytes);
                return results;
            }
        }

        public async Task<string> Put(string url, string data)
        {
            string results = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent queryString = new StringContent(data);
                var response = await httpClient.PutAsync(new Uri(url), queryString);
                Byte[] downloadedBytes = await response.Content.ReadAsByteArrayAsync();
                Encoding encoding = new ASCIIEncoding();
                results = encoding.GetString(downloadedBytes);
                return results;
            }
        }
    }
}
