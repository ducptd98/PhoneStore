using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class ProductAttributes
    {
        public long Id { get; set; }
        public int RAM { get; set; }
        public string CPU { get; set; }
        public double FrontCamera { get; set; }
        public double MainCamera { get; set; }
        public string Resolution { get; set; }
        public string ExternalStorage { get; set; }
        public int SIMCount { get; set; }
        public int PinCapacity { get; set; }
        public string OS { get; set; }
        public string Capacity { get; set; }
        public string MonitorSize { get; set; }
        [Display(Name ="2G")]
        public string Connection2G { get; set; }
        [Display(Name = "3G")]
        public string Connection3G { get; set; }
        [Display(Name = "4G")]
        public string Connection4G { get; set; }
        public string Wifi { get; set; }
        public string GPS { get; set; }
        public string Bluetooth { get; set; }
        [Display(Name ="Product Name")]
        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }

    }
}
