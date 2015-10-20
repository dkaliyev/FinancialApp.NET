using FinancialThing.Models;

namespace FinancialThing.Utilities
{
    public interface IDataGrabber
    {
        string Grab(string url);
    }
}