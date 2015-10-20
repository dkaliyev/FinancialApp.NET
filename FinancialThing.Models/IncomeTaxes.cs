namespace FinancialThing.Models
{
    public class IncomeTaxes: Entity
    {
        public Data NetIncomeBeforeTax { get; set; }
        public Data Provision { get; set; }
        public Data NetIncomeAfterTax { get; set; }
        public Data MinorityInterest { get; set; }
        public Data NetIncomeBeforeExtra { get; set; }
        public Data TotalExtraItems { get; set; }
        public Data NetIncome { get; set; }
        public Data CommonExclExtra { get; set; }
        public Data CommonInclExtra { get; set; }
    }
}