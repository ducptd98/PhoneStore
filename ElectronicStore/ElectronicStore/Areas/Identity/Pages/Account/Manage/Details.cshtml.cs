using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElectronicStore.Data;
using ElectronicStore.Models;

namespace ElectronicStore.Areas.Identity.Pages.Account.Manage
{
    public class DetailsModel : PageModel
    {
        private readonly ElectronicStore.Data.ApplicationDbContext _context;

        public DetailsModel(ElectronicStore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                .Include(o => o.Appointments)
                .Include(o => o.Products).FirstOrDefaultAsync(m => m.Id == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
