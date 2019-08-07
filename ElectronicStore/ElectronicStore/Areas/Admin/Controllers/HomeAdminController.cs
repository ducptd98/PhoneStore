using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : Controller
    {

        private readonly ApplicationDbContext _db;

        public IActionResult Index()
        {
            return View();
        }
    }
}