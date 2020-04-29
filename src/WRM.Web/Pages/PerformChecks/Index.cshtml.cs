using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WRM.App.NonNullChecks.Commands.PerformAllNonNullChecks;
using WRM.App.ReasonabilityChecks.Commands.PerformAllReasonabilityChecks;

namespace WRM.Web.Pages.PerformChecks
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [BindProperty]
        public DateTime DateOfCheck { get; set; }
        public void OnGet()
        {
            DateOfCheck = DateTime.Now.AddDays(-1);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _ = await _mediator.Send(new PerformAllReasonabilityChecksCommand() { CheckDate = DateOfCheck.Date });
            _ = await _mediator.Send(new PerformAllNonNullChecksCommand() { CheckDate = DateOfCheck.Date });
            return RedirectToPage();
        }
    }
}