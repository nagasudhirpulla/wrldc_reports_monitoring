using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WRM.App.CheckResults.Queries.GetAllCheckResultsForDate;

namespace WRM.Web
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public DateTime DateOfCheck { get; set; }
        public CheckResultsDTO Results { get; set; }

        private readonly IMediator _mediator;
        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync()
        {
            DateOfCheck = DateTime.Now.AddDays(-1);
            Results = await _mediator.Send(new GetAllCheckResultsForDateQuery() { DateOfCheck = DateOfCheck.Date });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Results = await _mediator.Send(new GetAllCheckResultsForDateQuery() { DateOfCheck = DateOfCheck.Date });
            return Page();
        }
    }
}