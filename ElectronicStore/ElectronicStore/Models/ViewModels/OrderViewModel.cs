using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }

        public Appointments Appointments { get; set; }

        public List<Products> Products { get; set; }

        public double subPrice { get; set; }

        public double discount { get; set; }
        public double totalPrice { get; set; }

        public int quantity { get; set; }
    }
}
