using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicStore.Data;
using ElectronicStore.Models;
using ElectronicStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductAttributesController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ProductAttributesViewModel AttributesVM { get; set; }
        public ProductAttributesController(ApplicationDbContext db)
        {
            _db = db;
            AttributesVM = new ProductAttributesViewModel()
            {
                Products = _db.Products.ToList(),
                ProductAttributes = new Models.ProductAttributes()
            };
        }
        public IActionResult Index()
        {
            var attributes = _db.ProductAttributes.Include(a => a.Products);
            return View(attributes.ToList());
        }
        #region Create
        //Get Create
        public IActionResult Create()
        {
            var prodsHaveAtt = (List<Products>)(from p in _db.Products
                                                join a in _db.ProductAttributes
                                                on p.Id equals a.ProductId
                                                select p).ToList();
            var productsList = _db.Products.ToList();
            //List<Products> position = new List<Products>();
            //foreach (var item in productsList)
            //{
            //    for(int i = 0; i < prodsHaveAtt.Count; i++)
            //    {
            //        if (item == prodsHaveAtt[i])
            //            position.Add(item);
            //    }
            //}
            //int j = 0;
            //while(position.Count!=0)
            //{
            //    productsList.Remove(position[j]);

            //    position.RemoveAt(j);
            //}
            var products = productsList.Except(prodsHaveAtt);
            AttributesVM.Products = products;
            return View(AttributesVM);
        }
        //Post Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                _db.ProductAttributes.Add(AttributesVM.ProductAttributes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(AttributesVM);

        }
        #endregion
        #region Edit
        public IActionResult Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            AttributesVM.ProductAttributes = _db.ProductAttributes.Include(a => a.Products).Where(a => a.Id == id).FirstOrDefault();
            if(AttributesVM.ProductAttributes == null)
            {
                return NotFound();
            }
            return View(AttributesVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id)
        {
            if(id != AttributesVM.ProductAttributes.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var attributeProdFromDb = await _db.ProductAttributes.Include(a => a.Products).Where(a => a.Id == id).FirstOrDefaultAsync();
                attributeProdFromDb.OS = AttributesVM.ProductAttributes.OS;
                attributeProdFromDb.CPU = AttributesVM.ProductAttributes.CPU;
                attributeProdFromDb.RAM = AttributesVM.ProductAttributes.RAM;
                attributeProdFromDb.MonitorSize = AttributesVM.ProductAttributes.MonitorSize;
                attributeProdFromDb.Resolution = AttributesVM.ProductAttributes.Resolution;
                attributeProdFromDb.MainCamera = AttributesVM.ProductAttributes.MainCamera;
                attributeProdFromDb.FrontCamera = AttributesVM.ProductAttributes.FrontCamera;
                attributeProdFromDb.Connection2G = AttributesVM.ProductAttributes.Connection2G;
                attributeProdFromDb.Connection3G = AttributesVM.ProductAttributes.Connection3G;
                attributeProdFromDb.Connection4G = AttributesVM.ProductAttributes.Connection4G;
                attributeProdFromDb.Wifi = AttributesVM.ProductAttributes.Wifi;
                attributeProdFromDb.Bluetooth = AttributesVM.ProductAttributes.Bluetooth;
                attributeProdFromDb.ExternalStorage = AttributesVM.ProductAttributes.ExternalStorage;
                attributeProdFromDb.SIMCount = AttributesVM.ProductAttributes.SIMCount;
                attributeProdFromDb.Capacity = AttributesVM.ProductAttributes.Capacity;
                attributeProdFromDb.PinCapacity = AttributesVM.ProductAttributes.PinCapacity;
                attributeProdFromDb.GPS = AttributesVM.ProductAttributes.GPS;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(AttributesVM);
        }
        #endregion
        #region Details
        public async Task<IActionResult> Details(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            AttributesVM.ProductAttributes = await _db.ProductAttributes.Include(a => a.Products).FirstOrDefaultAsync();
            if (AttributesVM.ProductAttributes == null)
            {
                return NotFound();
            }
            return View(AttributesVM);
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(long? id)
        {
            if(id ==null)
            {
                return NotFound();
            }
            var proAttrs = await _db.ProductAttributes.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(proAttrs == null)
            {
                return NotFound();
            }
            _db.ProductAttributes.Remove(proAttrs);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}