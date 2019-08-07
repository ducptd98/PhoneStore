using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicStore.Data;
using ElectronicStore.Extensions;
using ElectronicStore.Helper;
using ElectronicStore.Models;
using ElectronicStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ElectronicStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        //public ProductAttributesViewModel AttributeVM { get; set; }
        public DetaisViewModel DetailsVM { get; set; }

        public PagingInfo PagingInfo { get; set; }

        private int pageSize = 2;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
            //AttributeVM = new ProductAttributesViewModel()
            //{
            //    ProductAttributes = new Models.ProductAttributes(),
            //    Products = _db.Products.Include(p => p.Brands).Include(p => p.ProductCategory).ToList()
            //};
            DetailsVM = new DetaisViewModel
            {
                ProductAttributes = new Models.ProductAttributes(),
                Products = new Models.Products()
            };
        }
        public async Task<IActionResult> Index(string searchString, int pageNumber=1)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            ViewData["CurrentFilter"] = searchString;
            IQueryable<Products> products;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = _db.Products.Include(p => p.Brands).Include(p => p.ProductCategory)
                    .Where(p => p.Name.Contains(searchString) || p.Brands.Name.Contains(searchString) || p.ProductCategory.Name.Contains(searchString));
            }
            else
            {
                products = _db.Products.Include(p => p.Brands).Include(p => p.ProductCategory);
            }

            StringBuilder param = new StringBuilder();

            param.Append("/Customer/Home?pageNumber=:");
            param.Append("&searchString=");
            if (searchString != null)
            {
                param.Append(searchString);
            }

            var count = products.Count();

            PagingInfo = new PagingInfo()
            {
                Curpage = pageNumber,
                ItemPerPage = pageSize,
                TotalItems = count,
                urlParam = param.ToString()
            };
            @ViewData["PagingInfo"] = PagingInfo;
            //return View(await PaginatedList<Products>.CreateAsync(products,pageNumber??1,pageSize));
            return View(products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }
        //Details GET
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DetailsVM.Products = _db.Products.Include(p => p.Brands).Include(p => p.ProductCategory).Include(p=>p.MoreImages).Where(p => p.Id == id).FirstOrDefault();
            if (DetailsVM.Products == null)
            {
                return NotFound();
            }
            DetailsVM.ProductAttributes = _db.ProductAttributes.Where(a => a.ProductId == id).FirstOrDefault();
            if (DetailsVM.ProductAttributes == null)
            {
                return NotFound();
            }


            ViewData["DbContext"] = _db;
            //var relatedProduct = _db.Products.Include(p => p.Brands).Include(p => p.ProductCategory).Where(p => p.Brands.Name == DetailsVM.Products.Brands.Name).ToList();
            //ViewBag.relatedProd = relatedProduct;
            List<DetaisViewModel> details = new List<DetaisViewModel>();
            details.Add(DetailsVM);
            return View(details);
        }
        //Details POST
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsPost(int id)
        {
            List<long> lstShoppingCart = HttpContext.Session.Get<List<long>>("ssShoppingCart");
            if (lstShoppingCart == null)
            {
                lstShoppingCart = new List<long>();
            }
            if (!lstShoppingCart.Contains(id))
            { lstShoppingCart.Add(id); }
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}