using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicStore.Data;
using ElectronicStore.Models;
using ElectronicStore.Ultilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var listSuperAdmin = (IEnumerable<ApplicationUsers>)(from u in _db.ApplicationUsers
                                                                 join u_role in _db.UserRoles
                                                                 on u.Id equals u_role.UserId
                                                                 join r in _db.Roles
                                                                 on u_role.RoleId equals r.Id
                                                                 where r.Name !=SD.CustomerUser
                                                                 select u).ToList();


            return View(listSuperAdmin);
        }
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUsers userFromDb = _db.ApplicationUsers.Where(u => u.Id == id).FirstOrDefault();
            userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}