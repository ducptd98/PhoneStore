using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElectronicStore.Data;
using ElectronicStore.Models;

namespace ElectronicStore.Areas.Identity.Pages.Account.Manage
{
    public class YourOrdersModel : PageModel
    {
        private readonly ElectronicStore.Data.ApplicationDbContext _context;

        public YourOrdersModel(ElectronicStore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            
            Order = await _context.Orders.Include(o => o.Products)
                .Include(o => o.Appointments).ThenInclude(u=>u.Customer)
                .Where(u=>u.Appointments.Customer.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
               .ToListAsync();
        }
    }
}
