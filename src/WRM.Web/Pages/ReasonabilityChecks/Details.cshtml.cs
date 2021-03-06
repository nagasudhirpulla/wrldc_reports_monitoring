﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WRM.App.Data;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.ReasonabilityChecks
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public DetailsModel(WRM.App.Data.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
