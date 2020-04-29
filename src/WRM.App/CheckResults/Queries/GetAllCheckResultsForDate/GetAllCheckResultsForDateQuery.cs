using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WRM.App.CheckResults.Queries.GetAllCheckResultsForDate
{
    public class GetAllCheckResultsForDateQuery : IRequest<CheckResultsDTO>
    {
        public DateTime DateOfCheck { get; set; }
    }
}
