namespace FinancialThing.Models
{
    public class NetChangeInCash: Entity
    {
        public Data ForeignExchangeEffects { get; set; }
        public Data NetChange { get; set; }
        public Data NetCashBegin { get; set; }
        public Data NetCashEnd { get; set; }
    }
}