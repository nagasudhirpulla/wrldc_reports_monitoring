using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WRM.App.Data;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.PspMeasurements
{
    public class CreateModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public CreateModel(WRM.App.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PspMeasurement PspMeasurement { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PspMeasurements.Add(PspMeasurement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
