namespace FinancialThing.Models
{
    public class OperatingExpenses: Entity
    {
        public Data CostOfRevenueTotal { get; set; }
        public Data SelligTotal { get; set; }
        public Data DepreciationAmortization { get; set; }
        public Data UnusualExpenses { get; set; }
        public Data OtherExpensesTotal { get; set; }
        public Data TotalExpenses { get; set; }
        public Data OperatingIncome { get; set; }
        public Data OtherNet { get; set; }
    }
}