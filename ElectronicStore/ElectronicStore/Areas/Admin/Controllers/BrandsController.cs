using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ElectronicStore.Data;
using ElectronicStore.Models;
using ElectronicStore.Models.ViewModels;
using ElectronicStore.Ultilities;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnvironment;

        public BrandsController(ApplicationDbContext db, HostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
 
        }
        public IActionResult Index()
        {
            return View(_db.Brands.ToList());
        }

        //Get Create Action method
        public IActionResult Create()
        {
            return View();
        }
        //Post Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Brands brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }
            _db.Brands.Add(brand);
            await _db.SaveChangesAsync();

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var brandFromDb = _db.Brands.Find(brand.Id);
            if (files.Count != 0)
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolderBrand);
                var extension = Path.GetExtension(files[0].FileName);
                var names = brand.Name.ToLower().Split(" ");
                var completed_name = "";
                foreach (var item in names)
                {
                    completed_name += item;
                }
                using (var filestream = new FileStream(Path.Combine(uploads, completed_name + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                brand.Image = @"\" + SD.ImageFolderBrand + @"\" + completed_name + extension;
            }
            else
            {
                // when user does not upload image
                var uploads = Path.Combine(webRootPath, SD.ImageFolderBrand + @"\" + SD.DefaultProductImage);

                var names = brand.Name.ToLower().Split(" ");
                var completed_name = "";
                foreach (var item in names)
                {
                    completed_name += item;
                }
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolderBrand + @"\" + completed_name + ".png");
                brand.Image = @"\" + SD.ImageFolderBrand + @"\" + completed_name + ".png";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #region Edit
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brand = _db.Brands.Where(b => b.Id == id).FirstOrDefault();
            if (brand.Name != null)
            {
                brand.SeoTitle = brand.Name;
            }
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Brands brand)
        {
            if (brand.Id != id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(brand);
            }
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var brandFromDb = _db.Brands.Where(m => m.Id == brand.Id).FirstOrDefault();
            var names = brand.Name.ToLower().Split(" ");
            var completed_name = "";
            foreach (var item in names)
            {
                completed_name += item;
            }
            if (files.Count > 0 && files[0] != null)
            {


                //if user uploads  image
                var uploads = Path.Combine(webRootPath, SD.ImageFolderBrand);
                var extension_new = Path.GetExtension(files[0].FileName);
                var extension_old = Path.GetExtension(brandFromDb.Image);

                if (System.IO.File.Exists(Path.Combine(uploads, completed_name + extension_old)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, completed_name + extension_old));
                }
                using (var filestream = new FileStream(Path.Combine(uploads, completed_name + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                brand.Image = @"\" + SD.ImageFolderBrand + @"\" + completed_name + extension_new;

            }
            if (brand.Image != null)
            {
                brandFromDb.Image = brand.Image;
            }

            brandFromDb.Name = brand.Name;
            brandFromDb.MetaTitle = brand.MetaTitle;
            brandFromDb.SeoTitle = brand.SeoTitle;
            brandFromDb.MetaDescription = brand.MetaDescription;
            brandFromDb.MetaKeyword = brand.MetaKeyword;
            brandFromDb.ParenId = brand.ParenId;
            brandFromDb.DisplayOrder = brand.DisplayOrder;
            brandFromDb.ShowOnHome = brand.ShowOnHome;
            brandFromDb.Status = brand.Status;
            brandFromDb.ModifiedDate = brand.ModifiedDate;
            brandFromDb.ModifiedBy = brand.ModifiedBy;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        #endregion
        #region Details
        //Details
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brandFromDb = _db.Brands.Where(b => b.Id == id).FirstOrDefault();
            if (brandFromDb == null)
            {
                return NotFound();
            }
            return View(brandFromDb);
        }
        #endregion
        #region Delete
        //Delete
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brand = _db.Brands.Where(b => b.Id == id).FirstOrDefault();
            if (brand == null)
            {
                return NotFound();
            }
            _db.Brands.Remove(brand);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}