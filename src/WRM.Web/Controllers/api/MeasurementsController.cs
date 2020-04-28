using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WRM.App.PspMeasurements.Queries.GetAllPspMeasurements;
using WRM.Domain.Entities;

namespace WRM.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeasurementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/Measurements/getAll
        [HttpGet("getAll")]
        public async Task<IEnumerable<PspMeasurement>> GetAllMeasuremens()
        {
            List<PspMeasurement> measurements = await _mediator.Send(new GetAllPspMeasurementsQuery());
            return measurements;
        }
    }
}