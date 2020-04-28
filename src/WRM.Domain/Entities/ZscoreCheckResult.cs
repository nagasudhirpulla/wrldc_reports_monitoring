using System;

namespace WRM.Domain.Entities
{
    public class ZscoreCheckResult : BaseEntity
    {
        public ZscoreCheck ZscoreCheck { get; set; }
        public int ZscoreCheckId { get; set; }
        public DateTime DateOfCheck { get; set; }
        public bool IsPassed { get; set; }
        public double Violation { get; set; }
    }
}
