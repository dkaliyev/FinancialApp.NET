using System.Threading.Tasks;

namespace FinancialThing.Utilities
{
    public interface IDataGrabber
    {
        Task<string> Get(string url);

        Task<string> Put(string url, string data);

        Task<string> Post(string url, string data);

        Task<string> Delete(string url, string data);
    }
}