using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WRM.App.Data;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.PspMeasurements
{
    public class EditModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public EditModel(WRM.App.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PspMeasurement PspMeasurement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PspMeasurement = await _context.PspMeasurements.FirstOrDefaultAsync(m => m.Id == id);

            if (PspMeasurement == null)
            {
                return NotFound();
            }
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

            _context.Attach(PspMeasurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PspMeasurementExists(PspMeasurement.Id))
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

        private bool PspMeasurementExists(int id)
        {
            return _context.PspMeasurements.Any(e => e.Id == id);
        }
    }
}
