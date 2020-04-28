using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WRM.App.Data;
using WRM.App.Security;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.NonNullChecks
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class DeleteModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public DeleteModel(WRM.App.Data.AppDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NonNullCheck = await _context.NonNullChecks.FindAsync(id);

            if (NonNullCheck != null)
            {
                _context.NonNullChecks.Remove(NonNullCheck);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
