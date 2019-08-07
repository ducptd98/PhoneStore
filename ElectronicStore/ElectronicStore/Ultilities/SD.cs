using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Ultilities
{
    public class SD
    {
        public const string DefaultProductImage = "default_image.jpg";
        public const string DefaultBrandImage = "default_image.png";
        public const string ImageFolderProduct = @"images\ProductImages";
        public const string ImageFolderBrand = @"images\BrandImages";
        public const string SuperAdminEndUser = "Super Admin";
        public const string ShippingUser = "Vendor User";
        public const string CustomerUser = "Customer User";

        public enum Status
        {
            New = 0,
            Canceled =1,
            Complete = 2,
            Pending = 3,
            Delivery = 4
        }
    }
}
