namespace FinancialThing.Models
{
    public class RatioValue: Entity
    {
        public virtual string CompanyId { get; set; }
        public virtual string Value { get; set; }
        //public virtual string Year { get; set; }
        public virtual string RatioId { get; set; }
    }
}