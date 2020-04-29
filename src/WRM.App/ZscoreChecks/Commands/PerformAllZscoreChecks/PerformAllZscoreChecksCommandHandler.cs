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

namespace WRM.App.ZscoreChecks.Commands.PerformAllZscoreChecks
{
    public class PerformAllZscoreChecksCommandHandler : IRequestHandler<PerformAllZscoreChecksCommand, bool>
    {
        private readonly AppDbContext _context;
        private readonly IReportsFetchService _reportsFetchService;

        public PerformAllZscoreChecksCommandHandler(AppDbContext context, IReportsFetchService reportsFetchService)
        {
            _context = context;
            _reportsFetchService = reportsFetchService;
        }

        public async Task<bool> Handle(PerformAllZscoreChecksCommand request, CancellationToken cancellationToken)
        {
            // get all resonability checks
            List<ZscoreCheck> zscoreChecks = await _context.ZscoreChecks.Include(rc => rc.Measurement).ToListAsync();

            // iterate through each check for processing
            foreach (ZscoreCheck check in zscoreChecks)
            {
                // initialize result
                ZscoreCheckResult result = new ZscoreCheckResult()
                {
                    DateOfCheck = request.CheckDate,
                    IsPassed = false,
                    ZscoreCheckId = check.Id,
                    Violation = 0
                };
                
                int numDays = check.NumDays;
                // get data of measurement
                List<(DateTime, double)> measData = await _reportsFetchService.FetchTimeseriesData(check.Measurement.QueryString, check.Measurement.DateType, request.CheckDate, request.CheckDate.AddDays(-1 * numDays));

                if (measData.Count == 0)
                {
                    // data not present, hence failed
                }
                else
                {
                    measData = measData.OrderByDescending(m => m.Item1).ToList();
                    // check if we have the target date data
                    if ((measData.Count > 1) && (measData[0].Item1 == request.CheckDate))
                    {
                        // assumption - all previous data samples are not erroneous
                        double avg = measData.Skip(1).Select(m => m.Item2).Average();

                        // Perform the Sum of (value-avg)^2.      
                        double sum = measData.Skip(1).Select(m => m.Item2).Sum(d => Math.Pow(d - avg, 2));
                        // Put it all together.      
                        double std = Math.Sqrt((sum) / (measData.Count - 1));

                        double val = measData[0].Item2;
                        double zScore = (val - avg) / std;

                        if (Math.Abs(zScore) > Math.Abs(check.Threshold))
                        {
                            double violation = Math.Abs(zScore) - Math.Abs(check.Threshold);
                            if (zScore < 0)
                            {
                                violation *= -1;
                            }
                            result.Violation = violation;
                        }
                        else
                        {
                            result.IsPassed = true;
                        }
                    }
                }

                ZscoreCheckResult existingResult = await _context.ZscoreCheckResults
                                                            .Where(rcr => (rcr.DateOfCheck == request.CheckDate) && (rcr.ZscoreCheckId == check.Id))
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
                    _context.ZscoreCheckResults.Add(result);
                    await _context.SaveChangesAsync();
                }
            }
            return true;
        }

    }
}
