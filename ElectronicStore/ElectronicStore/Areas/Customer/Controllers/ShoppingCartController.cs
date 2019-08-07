using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectronicStore.Data;
using ElectronicStore.Extensions;
using ElectronicStore.Models;
using ElectronicStore.Models.ViewModels;
using ElectronicStore.Ultilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicStore.Areas.Admin.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Products>(),
                Appointments = new Appointments()
            };
        }
        //GET Index
        public async Task<IActionResult> Index()
        {
            List<long> lstShoppingCart = HttpContext.Session.Get<List<long>>("ssShoppingCart");
            if (lstShoppingCart == null) return View(ShoppingCartVM);
            if (lstShoppingCart.Count > 0)
            {
                foreach (var cartItem in lstShoppingCart)
                {
                    Products products = _db.Products.Include(p=>p.ProductCategory)
                        .Include(p=>p.Brands)
                        .Where(p => p.Id == cartItem).FirstOrDefault();
                    ShoppingCartVM.Products.Add(products);
                }

                ViewData["DbContext"] = _db;
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                ShoppingCartVM.Appointments.CustomerId = userId;
                ShoppingCartVM.Appointments.Customer = _db.ApplicationUsers.Where(u => u.Id == userId).FirstOrDefault();

            }
            
            return View(ShoppingCartVM);
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public IActionResult IndexPost()
        {
            List<long> lstShoppingCart = HttpContext.Session.Get<List<long>>("ssShoppingCart");
            ShoppingCartVM.Appointments.CreatedOn = DateTimeOffset.Now;
            ShoppingCartVM.Appointments.Status = (int)SD.Status.New;

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ShoppingCartVM.Appointments.CustomerId = userId;
            ShoppingCartVM.Appointments.Customer = _db.ApplicationUsers.Where(u => u.Id == userId).FirstOrDefault();
            ShoppingCartVM.Appointments.CreatedBy = _db.ApplicationUsers.Where(u => u.Id == userId).FirstOrDefault();
            ShoppingCartVM.Appointments.LatestUpdatedBy = _db.ApplicationUsers.Where(u => u.Id == userId).FirstOrDefault();
            //ShoppingCartVM.Appointments.CreatedBy.FullName = _db.Products.Find(productId).CreatedBy;

            //Appointments appointments = ShoppingCartVM.Appointments;
            _db.Appointments.Add(ShoppingCartVM.Appointments);
            
            _db.SaveChanges();

            long appointmentId = ShoppingCartVM.Appointments.Id;
            int countProduct = 0;
            foreach (long productId in lstShoppingCart)
            {
                
                Order order = new Order()
                {
                    ProductId = productId,
                    AppointmentId = appointmentId,
                    Quantity = Int32.Parse(HttpContext.Request.Form["quantity"][countProduct]),
                    Price = Convert.ToDecimal(_db.Products.Find(productId).Price * Int32.Parse(HttpContext.Request.Form["quantity"][countProduct]) * (1 - _db.Products.Find(productId).PromotionPrice / _db.Products.Find(productId).Price)),
                    Discount = Convert.ToDecimal((_db.Products.Find(productId).PromotionPrice / _db.Products.Find(productId).Price) * 100),
                    feeShip = Convert.ToDecimal(HttpContext.Request.Form["FeeShip"]) // co the loi
                };
                
                _db.Products.Find(productId).Quantity -= order.Quantity;
                
                _db.Orders.Add(order);
                countProduct++;
            }

            _db.SaveChanges();
            lstShoppingCart = new List<long>();
            countProduct = 0;
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            return RedirectToAction("Index");

        }
        public IActionResult Remove(long id)
        {
            List<long> lstShoppingCart = HttpContext.Session.Get<List<long>>("ssShoppingCart");
            if (lstShoppingCart.Count > 0)
            {
                if (lstShoppingCart.Contains(id))
                {
                    lstShoppingCart.Remove(id);
                }
            }
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);
            return RedirectToAction("Index");
        }
        ////POST
        //[HttpPost, ActionName("Confirm")]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> ConfirmPost()
        //{
        //    List<long> lstShoppingCart = HttpContext.Session.Get<List<long>>("ssShoppingCart");
        //    ShoppingCartVM.Appointments.CreatedOn = DateTimeOffset.Now;

        //    Appointments appointments = ShoppingCartVM.Appointments;
        //    _db.Appointments.Add(appointments);
        //    _db.SaveChanges();

        //    long appointmentId = appointments.Id;
        //    foreach (long productId in lstShoppingCart)
        //    {
        //        Order order = new Order()
        //        {
        //            ProductId = productId,
        //            AppointmentId = appointmentId,
        //            Quantity = Int32.Parse(HttpContext.Request.Form["quantity"]),
        //            Price = Convert.ToDecimal(_db.Products.Find(productId).Price)
        //        };
        //        _db.Products.Find(productId).Quantity -= order.Quantity;
        //        curOrder.Add(order);
        //        _db.Orders.Add(order);
        //    }

        //    _db.SaveChanges();
        //    lstShoppingCart = new List<long>();
        //    curOrder = new List<Order>();
        //    HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

        //    return RedirectToAction("Index");
        //}
    }
}