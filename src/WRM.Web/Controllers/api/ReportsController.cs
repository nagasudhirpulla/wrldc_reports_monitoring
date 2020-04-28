using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WRM.App.ReportsData.Queries.GetMeasurementData;

namespace WRM.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Startup.ApiAuthSchemes)]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public double ToMillisSinceUnixEpoch(DateTime time)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return time.ToUniversalTime().Subtract(UnixEpoch).TotalMilliseconds;
        }

        // GET api/Reports/getMeasData
        [HttpGet("getMeasData/{label}/{startTimeStr}/{endTimeStr}")]
        public async Task<IEnumerable<double>> GetAllMeasuremens(string label, string startTimeStr, string endTimeStr)
        {
            List<double> res = new List<double>();
            DateTime startTime = DateTime.ParseExact(startTimeStr, "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
            DateTime endTime = DateTime.ParseExact(endTimeStr, "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
            List<(DateTime, double)> data = await _mediator.Send(new GetMeasurementDataQuery() { StartTime = startTime, EndTime = endTime, MeasurementLabel = label });
            foreach ((DateTime, double) item in data)
            {
                res.Add(ToMillisSinceUnixEpoch(item.Item1));
                res.Add(item.Item2);
            }
            return res;
        }
    }
}