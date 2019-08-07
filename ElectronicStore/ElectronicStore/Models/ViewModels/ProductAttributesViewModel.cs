using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models.ViewModels
{
    public class ProductAttributesViewModel
    {
       public ProductAttributes ProductAttributes { get; set; }
       public IEnumerable<Products> Products { get; set; }
        
    }
}
