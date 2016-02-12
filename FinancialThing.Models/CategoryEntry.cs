namespace FinancialThing.Models
{
    public class CategoryEntry: Entity
    {
        public virtual GroupCategory GroupCategory { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Code { get; set; }
    }
}