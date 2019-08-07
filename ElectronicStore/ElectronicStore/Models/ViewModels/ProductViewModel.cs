using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ElectronicStore.Models.ViewModels
{
    public class ProductViewModel
    {
        public Products Products { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
        public IEnumerable<Brands> Brands { get; set; }

        public IList<IFormFile> Images { get; set; }
    }
}
