namespace FinancialThing.Models
{
    public class EPS: Entity
    {
        public Data BasicWeightedAverage { get; set; }
        public Data BasicEpsExclExtra { get; set; }
        public Data BasicEpsInclExtra { get; set; }
        public Data DilutionAdjustment { get; set; }
        public Data DilutedWeightedAv { get; set; }
        public Data DilutedEpsExclExtra { get; set; }
        public Data DilutedEpsInclExtra { get; set; }
    }
}