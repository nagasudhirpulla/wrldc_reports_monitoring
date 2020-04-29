using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WRM.App.Data;

namespace WRM.App.CheckResults.Queries.GetAllCheckResultsForDate
{
    public class GetAllPspMeasurementsQueryHandler : IRequestHandler<GetAllCheckResultsForDateQuery, CheckResultsDTO>
    {
        private readonly AppDbContext _context;

        public GetAllPspMeasurementsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CheckResultsDTO> Handle(GetAllCheckResultsForDateQuery request, CancellationToken cancellationToken)
        {
            var res = new CheckResultsDTO
            {
                NonNullCheckResults = await _context.NonNullCheckResults
                                        .Where(m => m.DateOfCheck == request.DateOfCheck)
                                        .Include(m => m.NonNullCheck)
                                        .ThenInclude(m => m.Measurement).ToListAsync(),
                ReasonabilityCheckResults = await _context.ReasonabilityCheckResults
                                            .Where(m => m.DateOfCheck == request.DateOfCheck)
                                            .Include(m => m.ReasonabilityCheck)
                                            .ThenInclude(m => m.Measurement).ToListAsync(),
                ZscoreCheckResults = await _context.ZscoreCheckResults
                                        .Where(m => m.DateOfCheck == request.DateOfCheck)
                                        .Include(m => m.ZscoreCheck)
                                        .ThenInclude(m => m.Measurement).ToListAsync()
            };
            return res;
        }
    }
}
