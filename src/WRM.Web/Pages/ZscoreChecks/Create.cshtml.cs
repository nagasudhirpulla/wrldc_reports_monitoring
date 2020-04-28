using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WRM.App.Data;
using WRM.App.Security;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.ZscoreChecks
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
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
        public ZscoreCheck ZscoreCheck { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ZscoreChecks.Add(ZscoreCheck);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
