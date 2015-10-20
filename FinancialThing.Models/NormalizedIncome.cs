namespace FinancialThing.Models
{
    public class NormalizedIncome: Entity
    {
        public Data BeforeTax { get; set; }
        public Data EffectOfSpecItems { get; set; }
        public Data IncomeTax { get; set; }
        public Data NormalizedIncomeAfterTax { get; set; }
        public Data NormalizedIncomeAvailable { get; set; }
        public Data BasicNormalizedEPS { get; set; }
        public Data DilutedNormalizedEPS { get; set; }
    }
}