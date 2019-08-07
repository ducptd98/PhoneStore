using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicStore.Data;
using ElectronicStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ProductCategories.ToList());
        }
        //Create Get
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post Create
        public async Task<IActionResult> Create(ProductCategory product)
        {
            if (ModelState.IsValid)
            {
                _db.ProductCategories.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        //Edit Get
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productCategoryFromDb = _db.ProductCategories.Where(c => c.Id == id).FirstOrDefault();
            if (productCategoryFromDb == null)
            {
                return NotFound();
            }
            return View(productCategoryFromDb);
        }
        //Edit Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ProductCategory productCategory)
        {
            if(productCategory.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if(productCategory.Name != null)
                {
                    productCategory.SeoTitle = productCategory.Name;
                }
                else
                {
                    productCategory.SeoTitle = productCategory.Id.ToString();
                }
                var prodCategoryFromDb = _db.ProductCategories.Where(c => c.Id == productCategory.Id).FirstOrDefault();
                prodCategoryFromDb.SeoTitle = productCategory.SeoTitle;
                prodCategoryFromDb.Name = productCategory.Name;
                prodCategoryFromDb.MetaTitle = productCategory.MetaTitle;
                prodCategoryFromDb.ParenId = productCategory.ParenId;
                prodCategoryFromDb.DisplayOrder = productCategory.DisplayOrder;
                prodCategoryFromDb.MetaKeyword = productCategory.MetaKeyword;
                prodCategoryFromDb.MetaDescription = productCategory.MetaDescription;
                prodCategoryFromDb.ModifiedDate = productCategory.ModifiedDate;
                prodCategoryFromDb.Status = productCategory.Status;
                prodCategoryFromDb.ShowOnHome = productCategory.ShowOnHome;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }
        //Get Details
        public IActionResult Details(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var productCategoryFromDb = _db.ProductCategories.Where(c => c.Id == id).FirstOrDefault();
            if(productCategoryFromDb == null)
            {
                return NotFound();
            }
            return View(productCategoryFromDb);
        }

        //Delete
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prodCategoryFromDb = _db.ProductCategories.Where(c => c.Id == id).FirstOrDefault();
            if (prodCategoryFromDb == null)
            {
                return NotFound();
            }
            _db.ProductCategories.Remove(prodCategoryFromDb);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}