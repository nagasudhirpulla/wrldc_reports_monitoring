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

namespace WRM.Web.Pages.ZscoreChecks
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ZscoreCheck = await _context.ZscoreChecks.FindAsync(id);

            if (ZscoreCheck != null)
            {
                _context.ZscoreChecks.Remove(ZscoreCheck);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
