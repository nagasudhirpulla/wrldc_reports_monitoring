using MediatR;
using System;
using System.Text;

namespace WRM.App.NonNullChecks.Commands.PerformAllNonNullChecks
{
    public class PerformAllNonNullChecksCommand : IRequest<bool>
    {
        public DateTime CheckDate { get; set; }
    }
}
