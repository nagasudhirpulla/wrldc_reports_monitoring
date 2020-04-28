using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WRM.App.Data;
using WRM.Domain.Entities;

namespace WRM.App.PspMeasurements.Queries.GetAllPspMeasurements
{

    public class GetAllPspMeasurementsQuery : IRequest<List<PspMeasurement>>
    {
        public class GetAllPspMeasurementsQueryHandler : IRequestHandler<GetAllPspMeasurementsQuery, List<PspMeasurement>>
        {
            private readonly AppDbContext _context;

            public GetAllPspMeasurementsQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<PspMeasurement>> Handle(GetAllPspMeasurementsQuery request, CancellationToken cancellationToken)
            {
                List<PspMeasurement> res = await _context.PspMeasurements.OrderBy(m => m.Label).ToListAsync();
                return res;
            }
        }
    }
}
