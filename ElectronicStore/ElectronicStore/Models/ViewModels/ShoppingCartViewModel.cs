﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Products> Products { get; set; }
        public Appointments Appointments { get; set; }

        public Order Order { get; set; }
    }
}
