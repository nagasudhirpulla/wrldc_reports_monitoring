using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WRM.Domain.Interfaces.Reports
{
    public interface IReportsFetchService
    {
        Task<List<(DateTime, double)>> FetchTimeseriesData(string queryString, string queryTimeType, DateTime startTime, DateTime endTime);
    }
}
