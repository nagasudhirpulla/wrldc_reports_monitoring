using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WRM.Domain.Entities;

namespace WRM.App.ReportsData.Queries.GetMeasurementData
{
    public class GetMeasurementDataQuery : IRequest<List<(DateTime, double)>>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string MeasurementLabel { get; set; }
    }
}
