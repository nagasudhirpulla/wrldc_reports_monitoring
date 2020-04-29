using MediatR;
using System;
using System.Text;

namespace WRM.App.ReasonabilityChecks.Commands.PerformAllReasonabilityChecks
{
    public class PerformAllReasonabilityChecksCommand : IRequest<bool>
    {
        public DateTime CheckDate { get; set; }
    }
}
