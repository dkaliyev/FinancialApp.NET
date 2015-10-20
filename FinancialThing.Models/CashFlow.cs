namespace FinancialThing.Models
{
    public class CashFlow: Entity
    {
        public Operations Operations { get; set; }
        public Investing Investing { get; set; }
        public Financing Financing { get; set; }
        public NetChangeInCash NetChangeInCash { get; set; }
        public SupplementalIncome SupplementalIncome { get; set; }
    }
}