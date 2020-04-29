using System;
using System.Collections.Generic;
using System.Text;
using WRM.Domain.Entities;

namespace WRM.App.CheckResults.Queries.GetAllCheckResultsForDate
{
    public class CheckResultsDTO
    {
        public List<NonNullCheckResult> NonNullCheckResults { get; set; } = new List<NonNullCheckResult>();
        public List<ReasonabilityCheckResult> ReasonabilityCheckResults { get; set; } = new List<ReasonabilityCheckResult>();
        public List<ZscoreCheckResult> ZscoreCheckResults { get; set; } = new List<ZscoreCheckResult>();
    }
}
