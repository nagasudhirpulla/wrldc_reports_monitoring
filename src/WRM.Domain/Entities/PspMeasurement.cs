namespace WRM.Domain.Entities
{
    public class PspMeasurement : BaseEntity
    {
        public string Label { get; set; }
        public string QueryString { get; set; }
        public string DateType { get; set; }
    }
}
