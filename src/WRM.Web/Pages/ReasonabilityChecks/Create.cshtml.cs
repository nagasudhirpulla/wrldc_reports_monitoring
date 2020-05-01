using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.ReasonabilityChecks
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public CreateModel(WRM.App.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MeasurementId"] = new SelectList(_context.PspMeasurements, "Id", "Label");
            return Page();
        }

        [BindProperty]
        public ReasonabilityCheck ReasonabilityCheck { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ReasonabilityChecks.Add(ReasonabilityCheck);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
