using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WRM.App.ZscoreChecks.Commands.PerformAllZscoreChecks
{
    public class PerformAllZscoreChecksCommand : IRequest<bool>
    {
        public DateTime CheckDate { get; set; }
    }
}
