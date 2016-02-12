namespace FinancialThing.Models
{
    public class Ratio: Entity
    {
        public virtual string Formula { get; set; }

        public virtual string Name { get; set; }
    }
}