using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WRM.App.Data;
using WRM.Domain.Entities;
using WRM.Domain.Interfaces.Reports;

namespace WRM.App.ReportsData.Queries.GetMeasurementData
{
    public class GetMeasurementDataQueryHandler : IRequestHandler<GetMeasurementDataQuery, List<(DateTime, double)>>
    {
        private readonly AppDbContext _context;
        private readonly IReportsFetchService _reportsFetchService;

        public GetMeasurementDataQueryHandler(AppDbContext context, IReportsFetchService reportsFetchService)
        {
            _context = context;
            _reportsFetchService = reportsFetchService;
        }

        public async Task<List<(DateTime, double)>> Handle(GetMeasurementDataQuery request, CancellationToken cancellationToken)
        {
            // get the measurement
            PspMeasurement meas = await _context.PspMeasurements.Where(m => m.Label == request.MeasurementLabel).FirstOrDefaultAsync();
            if (meas == null)
            {
                return new List<(DateTime, double)>();
            }
            List<(DateTime, double)> res = await _reportsFetchService.FetchTimeseriesData(meas.QueryString, meas.DateType, request.StartTime, request.EndTime);
            return res;
        }
    }
}
