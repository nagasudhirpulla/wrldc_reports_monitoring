using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WRM.App.Data;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.NonNullChecks
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public DetailsModel(WRM.App.Data.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
