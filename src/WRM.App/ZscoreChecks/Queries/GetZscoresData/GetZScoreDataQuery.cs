using MediatR;
using System;
using System.Text;
using WRM.App.Data;
using WRM.Domain.Entities;

namespace WRM.App.ZscoreChecks.Queries.GetZscoresData
{
    public class GetZScoreDataQuery : IRequest<ZscoresDTO>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public PspMeasurement Measurement { get; set; }
        public int NumDays { get; set; }
    }
}
