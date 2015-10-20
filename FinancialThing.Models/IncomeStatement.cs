using System.Runtime.ConstrainedExecution;

namespace FinancialThing.Models
{
    public class IncomeStatement: Entity
    {
        public RevenueAndGrossProfit RevenueAndGrossProfit { get; set; }
        public OperatingExpenses OperatingExpenses { get; set; }
        public IncomeTaxes IncomeTaxes { get; set; }
        public EPS Eps { get; set; }
        public CommonStockDividends CommonStockDividends { get; set; }
        public ProFormaIncome ProFormaIncome { get; set; }
        public SupplementalIncomeIS SupplementalIncomeIs { get; set; }
        public NormalizedIncome NormalizedIncome { get; set; }
    }
}