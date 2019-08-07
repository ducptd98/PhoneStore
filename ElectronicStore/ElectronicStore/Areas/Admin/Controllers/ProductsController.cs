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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnvironment;
        [BindProperty]
        public ProductViewModel ProductVM { get; set; }
        public ProductsController(ApplicationDbContext db, HostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            ProductVM = new ProductViewModel()
            {
                ProductCategories = _db.ProductCategories.ToList(),
                Brands = _db.Brands.ToList(),
                Images = new List<IFormFile>(),
                Products = new Products()
            };
        }
        public IActionResult Index()
        {
            var prodFromDb = _db.Products.Include(p => p.ProductCategory).Include(p => p.Brands).Include(p=>p.MoreImages);
            return View(prodFromDb.ToList());
        }
        #region Create
        //Create GET Action Method
        public IActionResult Create()
        {
            return View(ProductVM);
        }
        //Create POST
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(IList<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return View(ProductVM);
            }
            _db.Products.Add(ProductVM.Products);
            //await _db.SaveChangesAsync();

            string webRootPath = _hostingEnvironment.WebRootPath;
            //var files = HttpContext.Request.Form.Files;
            var prodFromDb = _db.Products.Find(ProductVM.Products.Id);
            if (files.Count != 0)
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolderProduct);
                var extension = Path.GetExtension(files[0].FileName);
                var names = ProductVM.Products.Code.ToLower().Split(" ");
                var completed_name = "";
                foreach (var item in names)
                {
                    completed_name += item;
                }
                using (var filestream = new FileStream(Path.Combine(uploads, completed_name + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                ProductVM.Products.Images = @"\" + SD.ImageFolderProduct + @"\" + completed_name + extension;
                await UploadProductImages(files, prodFromDb.Name, prodFromDb.Id);
            }
            else
            {
                // when user does not upload image
                var uploads = Path.Combine(webRootPath, SD.ImageFolderProduct + @"\" + SD.DefaultProductImage);

                var names = ProductVM.Products.Name.ToLower().Split(" ");
                var completed_name = "";
                foreach (var item in names)
                {
                    completed_name += item;
                }
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolderProduct + @"\" + completed_name + ".jpg");
                ProductVM.Products.Images = @"\" + SD.ImageFolderProduct + @"\" + completed_name + ".jpg";
            }
            if(ProductVM.Products.Images != null)
            {
                prodFromDb.Images = ProductVM.Products.Images;
            }
            if(prodFromDb.Quantity > 0)
            {
                prodFromDb.Status = true;
            }
            if (prodFromDb.Name != null)
            {
                prodFromDb.SEOTitle = prodFromDb.Name;
                prodFromDb.MetaTitle = prodFromDb.Name;
                prodFromDb.MetaKeyword = prodFromDb.Name;
                prodFromDb.MetaDescription = prodFromDb.ShortDescription;
                _db.SaveChanges();
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Edit
        //Edit GET
        public IActionResult Edit(long? id)
        {
            if(id ==null)
            {
                return NotFound();
            }
            ProductVM.Products = _db.Products.Include(p => p.ProductCategory).Include(p => p.Brands).SingleOrDefault(m=>m.Id == id);
            if(ProductVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductVM);
        }
        //Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, IList<IFormFile> files)
        {
            if(id != ProductVM.Products.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                //var files = HttpContext.Request.Form.Files;
                var prodFromDb = _db.Products.Where(m => m.Id == ProductVM.Products.Id).FirstOrDefault();
                //var names = prodFromDb.Name.ToLower().Split(" ");
                //var completed_name = "";
                //foreach (var item in names)
                //{
                //    completed_name += item;
                //}
                if (files.Count > 0 && files[0] != null)
                {


                    //if user uploads  image
                    var uploads = Path.Combine(webRootPath, SD.ImageFolderProduct);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(prodFromDb.Images);

                    if (System.IO.File.Exists(Path.Combine(uploads, files[0].FileName)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, files[0].FileName));
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, files[0].FileName), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    ProductVM.Products.Images = @"\" + SD.ImageFolderProduct + @"\" + files[0].FileName;
                    await UploadProductImages(files, prodFromDb.Name, prodFromDb.Id);
                }
                if(ProductVM.Products.Images != null)
                {
                    prodFromDb.Images = ProductVM.Products.Images;
                }
                prodFromDb.Name = ProductVM.Products.Name;
                prodFromDb.Code = ProductVM.Products.Code;
                prodFromDb.Price = ProductVM.Products.Price;
                prodFromDb.PromotionPrice = ProductVM.Products.PromotionPrice;
                prodFromDb.Warranty = ProductVM.Products.Warranty;
                prodFromDb.CategoryId = ProductVM.Products.CategoryId;
                prodFromDb.BrandId = ProductVM.Products.BrandId;
                prodFromDb.ShortDescription = ProductVM.Products.ShortDescription;
                prodFromDb.Description = ProductVM.Products.Description;
                prodFromDb.MetaTitle = ProductVM.Products.MetaTitle;
                prodFromDb.MetaKeyword = ProductVM.Products.MetaKeyword;
                prodFromDb.MetaDescription = ProductVM.Products.MetaDescription;
                prodFromDb.SEOTitle = ProductVM.Products.SEOTitle;
                prodFromDb.Quantity = ProductVM.Products.Quantity;
                prodFromDb.ShowOnHome = ProductVM.Products.ShowOnHome;
                if (prodFromDb.Quantity > 0)
                {
                    prodFromDb.Status = true;
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           }
            return View(ProductVM);
        }
        #endregion
        #region Details
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductVM.Products = _db.Products.Include(p => p.ProductCategory).Include(p => p.Brands).Include(p=>p.MoreImages).SingleOrDefault(m => m.Id == id);
            if (ProductVM.Products == null)
            {
                return NotFound();
            }
            return View(ProductVM);
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _db.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        private async Task UploadProductImages(IList<IFormFile> list, string productName, long productId)
        {
            //xoá hết cũ
            //foreach (var image in _db.ProductImages.Where(i => i.ProductId == productId))
            //{
            //    _db.ProductImages.Remove(image);
            //}

            //await _db.SaveChangesAsync();


            foreach (var file in list)
            {
                if (file != null && (file.ContentType == "image/png" || file.ContentType == "image/jpg" || file.ContentType == "image/jpeg"))
                {
                    var productImage = new ProductImage
                    {
                        AddedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ProductId = productId,
                        Title = productName
                    };

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, SD.ImageFolderProduct);
                    //var fileName = Path.Combine(uploads, productName + "_" + Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-", StringComparison.Ordinal)) + ".png");
                    var fileName = Path.Combine(uploads, file.FileName);

                    //Model
                    productImage.ImagePath = @"\" + SD.ImageFolderProduct + @"\"+Path.GetFileName(fileName);
                    if (System.IO.File.Exists(Path.Combine(uploads, fileName)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, fileName));
                    }

                    if (_db.ProductImages.Where(i => i.ProductId == productId).Count() == 0)
                    {
                        _db.ProductImages.Add(productImage);
                    }
                    else
                    {
                        var pImage = _db.ProductImages.Where(i => i.ProductId == productId).FirstOrDefault();
                        pImage.ProductId = productImage.ProductId;
                        pImage.Title = productImage.Title;
                        pImage.ModifiedDate = DateTime.Now;
                        pImage.ImagePath = productImage.ImagePath;
                        await _db.SaveChangesAsync();
                    }
                   
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}