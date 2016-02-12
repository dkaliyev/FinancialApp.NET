namespace FinancialThing.Models
{
    public class Industry: Entity
    {
        public virtual string DisplayName { get; set; }
        public virtual string Code { get; set; }
        public virtual Sector Sector { get; set; }
    }
}