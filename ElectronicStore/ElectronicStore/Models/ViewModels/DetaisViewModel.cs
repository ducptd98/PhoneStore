using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models.ViewModels
{
    public class DetaisViewModel
    {
        public ProductAttributes ProductAttributes { get; set; }
        public Products Products { get; set; }

        public int Quantity { get; set; }
    }
}
