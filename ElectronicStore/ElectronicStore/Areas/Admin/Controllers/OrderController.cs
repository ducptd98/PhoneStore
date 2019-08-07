using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectronicStore.Data;
using ElectronicStore.Extensions;
using ElectronicStore.Helper;
using ElectronicStore.Models;
using ElectronicStore.Models.ViewModels;
using ElectronicStore.Ultilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace ElectronicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public OrderViewModel OrderVM { get; set; }

        public objProduct ObjProduct { get; set; }

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
            OrderVM = new OrderViewModel()
            {
                Products = new List<Products>(),
                Order = new Order(),
                Appointments = new Appointments()
            };
            ObjProduct = new objProduct();
        }
        public IActionResult Index()
        {
            //var lstOrder = (from u in _db.ApplicationUsers
            //                join a in _db.Appointments on u.Id equals a.CustomerId
            //                join or in _db.Orders on a.Id equals or.AppointmentId
            //                join p in _db.Products on or.ProductId equals p.Id
            //                select new OrderViewModel()
            //                {
            //                    ApplicationUser = u,
            //                    Products = p,
            //                    Order = or,
            //                    Appointments = a
            //                }).ToList();
            var lstOrder = _db.Orders.Include(p => p.Products)
                .Include(a => a.Appointments)
                .ThenInclude(u => u.Customer).ToList();

            return View(lstOrder);
        }

        public IActionResult Create()
        {
            ViewData["DbContext"] = _db;
            return View(OrderVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreatePost()
        {
            List<long> lstOrderProductId = HttpContext.Session.Get<List<long>>("ssOrderProductId"); //danh sach productId

            return RedirectToAction("Index");
        }
        public IActionResult Edit(long? id)
        {
            if (id == null) return NotFound();
            var order = _db.Orders.Include(p => p.Products)
                .Include(a => a.Appointments)
                .ThenInclude(u => u.Customer)
                .Include(a1=>a1.Appointments).ThenInclude(s=>s.LatestUpdatedBy)
                .Where(o=>o.Id==id).FirstOrDefault();
            ViewData["DbContext"] = _db;
            return View(order);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditPost(long id)
        {
            var orderfromDb = _db.Orders.Include(p => p.Products)
                .Include(a => a.Appointments)
                .ThenInclude(u => u.Customer).Where(o => o.Id == id).FirstOrDefault();
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            orderfromDb.Appointments.LatestUpdatedBy = _db.ApplicationUsers.Where(u => u.Id == userId).FirstOrDefault();
            orderfromDb.Appointments.Status = OrderVM.Appointments.Status;
            _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public JsonResult Search(string Prefix)
        {
            var userList = _db.ApplicationUsers.Where(p => p.FullName.Contains(Prefix)).ToList();
            return Json(userList);
        }

        public JsonResult SearchProduct(string Prefix)
        {
            var productList = _db.Products.Where(p => p.Name.Contains(Prefix)).ToList();
            return Json(productList);
        }

        public IActionResult addProductToList(long id)
        {
            List<objProduct> lstOrderProductId = HttpContext.Session.Get<List<objProduct>>("ssOrderProductId");
            if (lstOrderProductId == null)
            {
                lstOrderProductId = new List<objProduct>();
            }

            ObjProduct.Id = id;
            if (!lstOrderProductId.Contains(ObjProduct))
            {
                lstOrderProductId.Add(ObjProduct);
            }
            HttpContext.Session.Set("ssOrderProductId", lstOrderProductId);
            return RedirectToAction("Create");
        }
        
        public ActionResult TableData()
        {
            List<objProduct> lstOrderProductId = HttpContext.Session.Get<List<objProduct>>("ssOrderProductId");
            if (lstOrderProductId == null)
            {
                return null;
            }
            foreach (var orderObjProduct in lstOrderProductId)
            {
                Products productsTemp = _db.Products.Include(p => p.ProductCategory)
                    .Include(p => p.Brands)
                    .Where(p => p.Id == orderObjProduct.Id).FirstOrDefault();
                OrderVM.Products.Add(productsTemp);
                OrderVM.subPrice += orderObjProduct.Quantity * (productsTemp.PromotionPrice>0?productsTemp.PromotionPrice:productsTemp.Price);
                OrderVM.totalPrice = OrderVM.subPrice * (1 - OrderVM.discount);
            }
            return PartialView("_TableProductPartial", OrderVM);
        }

        public IActionResult Remove(long id)
        {
            List<objProduct> lstOrderProductId = HttpContext.Session.Get<List<objProduct>>("ssOrderProductId");
            if (ObjProduct.Id != id) return NotFound();
            if (lstOrderProductId.Count > 0)
            {
                foreach (var obj in lstOrderProductId)
                {
                    if (obj.Id == id)
                    {
                        lstOrderProductId.Remove(obj);
                        Products productsTemp = _db.Products.Include(p => p.ProductCategory)
                            .Include(p => p.Brands)
                            .Where(p => p.Id == obj.Id).FirstOrDefault();
                        OrderVM.Products.Remove(productsTemp);
                    }
                    break;
                }
            }
            HttpContext.Session.Set("ssOrderProductId", lstOrderProductId);
            return View("Create");
        }
    }
}