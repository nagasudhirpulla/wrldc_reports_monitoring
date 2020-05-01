using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.ReasonabilityChecks
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public DeleteModel(WRM.App.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReasonabilityCheck ReasonabilityCheck { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReasonabilityCheck = await _context.ReasonabilityChecks
                .Include(r => r.Measurement).FirstOrDefaultAsync(m => m.Id == id);

            if (ReasonabilityCheck == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReasonabilityCheck = await _context.ReasonabilityChecks.FindAsync(id);

            if (ReasonabilityCheck != null)
            {
                _context.ReasonabilityChecks.Remove(ReasonabilityCheck);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
