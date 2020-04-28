using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WRM.App.Data;
using WRM.App.Security;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.NonNullChecks
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class EditModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public EditModel(WRM.App.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NonNullCheck NonNullCheck { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NonNullCheck = await _context.NonNullChecks
                .Include(n => n.Measurement).FirstOrDefaultAsync(m => m.Id == id);

            if (NonNullCheck == null)
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

            _context.Attach(NonNullCheck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NonNullCheckExists(NonNullCheck.Id))
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

        private bool NonNullCheckExists(int id)
        {
            return _context.NonNullChecks.Any(e => e.Id == id);
        }
    }
}
