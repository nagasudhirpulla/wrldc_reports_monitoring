using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WRM.App.Data;
using WRM.Domain.Entities;

namespace WRM.Web.Pages.PspMeasurements
{
    public class IndexModel : PageModel
    {
        private readonly WRM.App.Data.AppDbContext _context;

        public IndexModel(WRM.App.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<PspMeasurement> PspMeasurement { get;set; }

        public async Task OnGetAsync()
        {
            PspMeasurement = await _context.PspMeasurements.ToListAsync();
        }
    }
}
