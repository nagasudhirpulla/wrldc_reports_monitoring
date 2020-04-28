namespace WRM.Domain.Entities
{
    public class NonNullCheck : BaseEntity
    {
        public PspMeasurement Measurement { get; set; }
        public int MeasurementId { get; set; }
    }
}
