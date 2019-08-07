using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class Contents
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }
        public string Detail { get; set; }
        public string Warranty { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public String ModifiedBy { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool Status { get; set; }
        public bool ShowOnHome { get; set; }
        public DateTime TopHot { get; set; }
        public int ViewCount { get; set; }
        public string Tags { get; set; }
    }
}
