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

namespace WRM.App.NonNullChecks.Commands.PerformAllNonNullChecks
{
    public class PerformAllNonNullChecksCommandHandler : IRequestHandler<PerformAllNonNullChecksCommand, bool>
    {
        private readonly AppDbContext _context;
        private readonly IReportsFetchService _reportsFetchService;

        public PerformAllNonNullChecksCommandHandler(AppDbContext context, IReportsFetchService reportsFetchService)
        {
            _context = context;
            _reportsFetchService = reportsFetchService;
        }

        public async Task<bool> Handle(PerformAllNonNullChecksCommand request, CancellationToken cancellationToken)
        {
            // get all resonability checks
            List<NonNullCheck> nonNullChecks = await _context.NonNullChecks.Include(rc => rc.Measurement).ToListAsync();

            // iterate through each check for processing
            foreach (NonNullCheck check in nonNullChecks)
            {
                // initialize result
                NonNullCheckResult result = new NonNullCheckResult()
                {
                    DateOfCheck = request.CheckDate,
                    IsPassed = false,
                    NonNullCheckId = check.Id,
                };

                // get data of measurement
                List<(DateTime, double)> measData = await _reportsFetchService.FetchTimeseriesData(check.Measurement.QueryString, check.Measurement.DateType, request.CheckDate, request.CheckDate);

                if (measData.Count == 0)
                {
                    // data not present, hence failed
                }
                else
                {
                    result.IsPassed = true;
                }

                NonNullCheckResult existingResult = await _context.NonNullCheckResults
                                                            .Where(nnc => (nnc.DateOfCheck == request.CheckDate) && (nnc.NonNullCheckId == check.Id))
                                                            .FirstOrDefaultAsync();
                if (existingResult != null)
                {
                    // update existing check result for the date
                    existingResult.IsPassed = result.IsPassed;

                    // update changes to db
                    _context.Attach(existingResult).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // create the check result and push to db
                    _context.NonNullCheckResults.Add(result);
                    await _context.SaveChangesAsync();
                }
            }
            return true;
        }

    }
}
