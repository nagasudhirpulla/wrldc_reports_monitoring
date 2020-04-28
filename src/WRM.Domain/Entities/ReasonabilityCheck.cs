namespace WRM.Domain.Entities
{
    public class ReasonabilityCheck : BaseEntity
    {
        public PspMeasurement Measurement { get; set; }
        public int MeasurementId { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
