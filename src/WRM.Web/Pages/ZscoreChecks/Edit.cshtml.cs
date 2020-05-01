using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.ZscoreChecks
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public EditModel(WRM.App.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ZscoreCheck ZscoreCheck { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ZscoreCheck = await _context.ZscoreChecks
                .Include(z => z.Measurement).FirstOrDefaultAsync(m => m.Id == id);

            if (ZscoreCheck == null)
            {
                return NotFound();
            }
           ViewData["MeasurementId"] = new SelectList(_context.PspMeasurements, "Id", "Label");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ZscoreCheck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZscoreCheckExists(ZscoreCheck.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ZscoreCheckExists(int id)
        {
            return _context.ZscoreChecks.Any(e => e.Id == id);
        }
    }
}
