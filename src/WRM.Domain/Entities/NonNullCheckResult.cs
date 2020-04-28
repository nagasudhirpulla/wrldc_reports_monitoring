using System;

namespace WRM.Domain.Entities
{
    public class NonNullCheckResult : BaseEntity
    {
        public NonNullCheck NonNullCheck { get; set; }
        public int NonNullCheckId { get; set; }
        public DateTime DateOfCheck { get; set; }
        public bool IsPassed { get; set; }
    }
}
