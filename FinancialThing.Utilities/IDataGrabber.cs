namespace FinancialThing.Utilities
{
    public interface IDataGrabber
    {
        string Grab(string url);
        void Put(string url, string data);

        string Post(string url, string data);
    }
}