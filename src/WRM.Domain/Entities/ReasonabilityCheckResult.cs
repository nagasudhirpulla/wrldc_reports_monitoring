using System;

namespace WRM.Domain.Entities
{
    public class ReasonabilityCheckResult : BaseEntity
    {
        public ReasonabilityCheck ReasonabilityCheck { get; set; }
        public int ReasonabilityCheckId { get; set; }
        public DateTime DateOfCheck { get; set; }
        public bool IsPassed { get; set; }
        public double Violation { get; set; }
    }
}
