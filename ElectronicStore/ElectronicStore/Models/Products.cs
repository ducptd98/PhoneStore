using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class Products
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public string MetaTitle { get; set; }
        public string SEOTitle { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public double Price { get; set; }
        public double PromotionPrice { get; set; }
        public bool IncludeVAT { get; set; }
        public int Quantity { get; set; }
        [Display(Name ="Category")]
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }
        [Display(Name="Brands")]
        public long BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brands Brands { get; set; }
        public string Warranty { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public String ModifiedBy { get; set; }
        public string MetaKeyword { get; set; }
        public string ShortDescription { get; set; }
        public string MetaDescription { get; set; }
        public bool Status { get; set; }
        public bool ShowOnHome { get; set; }
        public DateTime TopHot { get; set; }
        public int ViewCount { get; set; }

        public ICollection<ProductImage> MoreImages { get; set; }
    }
}
