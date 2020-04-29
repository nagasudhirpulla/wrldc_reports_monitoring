using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WRM.Domain.Interfaces.Reports;

namespace WRM.App.ZscoreChecks.Queries.GetZscoresData
{
    public class GetZScoreDataQueryHandler : IRequestHandler<GetZScoreDataQuery, ZscoresDTO>
    {
        private readonly IReportsFetchService _reportsFetchService;

        public GetZScoreDataQueryHandler(IReportsFetchService reportsFetchService)
        {
            _reportsFetchService = reportsFetchService;
        }


        public async Task<ZscoresDTO> Handle(GetZScoreDataQuery request, CancellationToken cancellationToken)
        {
            List<(DateTime, double)> measData = await _reportsFetchService.FetchTimeseriesData(request.Measurement.QueryString, request.Measurement.DateType, request.StartTime, request.EndTime);

            measData = measData.OrderBy(m => m.Item1).ToList();

            // extract data
            List<double> data = measData.Select(m => m.Item2).ToList();

            // calculate zscores
            List<double> zscores = data.Take(request.NumDays).Select(d=>0.0).ToList();
            for (int i = request.NumDays; i < data.Count; i++)
            {
                // assumption - all previous data samples are not erroneous
                IEnumerable<double> reqData = data.Skip(i - request.NumDays).Take(request.NumDays);
                double avg = reqData.Average();
                // Perform the Sum of (value-avg)^2.      
                double sum = reqData.Sum(d => Math.Pow(d - avg, 2));
                // Put it all together.      
                double std = Math.Sqrt((sum) / (reqData.Count()));
                double zScore = (data[i] - avg) / std;
                zscores.Add(zScore);
            }

            return new ZscoresDTO()
            {
                zScores = zscores,
                vals = data,
                Timestamps = measData.Select(m => m.Item1).ToList()
            };

        }
    }
}
