namespace WRM.Domain.Entities
{
    public class ZscoreCheck : BaseEntity
    {
        public PspMeasurement Measurement { get; set; }
        public int MeasurementId { get; set; }
        public int NumDays { get; set; }
        public double Threshold { get; set; }
        public double Influence { get; set; }
    }
}
