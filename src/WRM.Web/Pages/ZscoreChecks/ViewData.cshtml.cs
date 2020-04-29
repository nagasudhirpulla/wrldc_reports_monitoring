using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WRM.App.Data;
using WRM.App.ZscoreChecks.Queries.GetZscoresData;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.ZscoreChecks
{
    [Authorize]
    public class ViewDataModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;
        public ViewDataModel(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        [BindProperty]
        public int NumDays { get; set; }
        [BindProperty]
        public int MeasurementId { get; set; }

        public ZscoresDTO ZscoresData { get; set; }
        public void OnGet()
        {
            ViewData["MeasurementId"] = new SelectList(_context.PspMeasurements, "Id", "Label");
            EndDate = DateTime.Now.AddDays(-1);
            StartDate = DateTime.Now.AddMonths(-6);
            NumDays = 15;
            ZscoresData = new ZscoresDTO();
        }

        public async Task OnPostAsync()
        {
            ViewData["MeasurementId"] = new SelectList(_context.PspMeasurements, "Id", "Label");
            PspMeasurement measurement = await _context.PspMeasurements.Where(m => m.Id == MeasurementId).FirstOrDefaultAsync();
            ZscoresData = await _mediator.Send(new GetZScoreDataQuery() { StartTime = StartDate.Date, EndTime = EndDate.Date, Measurement = measurement, NumDays = NumDays });
        }
    }
}