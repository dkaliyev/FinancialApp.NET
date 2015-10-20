namespace FinancialThing.Models
{
    public class Financing: Entity     
    {
        public Data CashFlowItems { get; set; }
        public Data TotalCashDividendsPaid { get; set; }
        public Data IssuanceStockNet { get; set; }
        public Data IssuanceDebtNet { get; set; }
        public Data TotalCash { get; set; }
    }
}