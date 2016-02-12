namespace FinancialThing.Models
{
    public class Operation
    {
        public float Value { get; set; }
        public virtual Operation Execute()
        {
            return null;
        }
    }
}