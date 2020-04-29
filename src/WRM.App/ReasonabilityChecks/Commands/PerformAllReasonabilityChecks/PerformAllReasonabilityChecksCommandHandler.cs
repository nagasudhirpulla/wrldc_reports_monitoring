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

namespace WRM.App.ReasonabilityChecks.Commands.PerformAllReasonabilityChecks
{
    public class PerformAllReasonabilityChecksCommandHandler : IRequestHandler<PerformAllReasonabilityChecksCommand, bool>
    {
        private readonly AppDbContext _context;
        private readonly IReportsFetchService _reportsFetchService;

        public PerformAllReasonabilityChecksCommandHandler(AppDbContext context, IReportsFetchService reportsFetchService)
        {
            _context = context;
            _reportsFetchService = reportsFetchService;
        }

        public async Task<bool> Handle(PerformAllReasonabilityChecksCommand request, CancellationToken cancellationToken)
        {
            // get all resonability checks
            List<ReasonabilityCheck> reasonabilityChecks = await _context.ReasonabilityChecks.Include(rc => rc.Measurement).ToListAsync();

            // iterate through each check for processing
            foreach (ReasonabilityCheck check in reasonabilityChecks)
            {
                // initialize result
                ReasonabilityCheckResult result = new ReasonabilityCheckResult()
                {
                    DateOfCheck = request.CheckDate,
                    IsPassed = false,
                    ReasonabilityCheckId = check.Id,
                    Violation = 0
                };

                // get data of measurement
                List<(DateTime, double)> measData = await _reportsFetchService.FetchTimeseriesData(check.Measurement.QueryString, check.Measurement.DateType, request.CheckDate, request.CheckDate);

                if (measData.Count == 0)
                {
                    // data not present, hence failed
                }
                else
                {
                    double val = measData[0].Item2;
                    if (val > check.MaxValue)
                    {
                        result.Violation = val - check.MaxValue;
                    }
                    else if (val < check.MinValue)
                    {
                        result.Violation = val - check.MinValue;
                    }
                    else
                    {
                        result.IsPassed = true;
                    }
                }

                ReasonabilityCheckResult existingResult = await _context.ReasonabilityCheckResults
                                                            .Where(rcr => (rcr.DateOfCheck == request.CheckDate) && (rcr.ReasonabilityCheckId == check.Id))
                                                            .FirstOrDefaultAsync();
                if (existingResult != null)
                {
                    // update existing check result for the date
                    existingResult.Violation = result.Violation;
                    existingResult.IsPassed = result.IsPassed;

                    // update changes to db
                    _context.Attach(existingResult).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // create the check result and push to db
                    _context.ReasonabilityCheckResults.Add(result);
                    await _context.SaveChangesAsync();
                }
            }
            return true;
        }

    }
}
