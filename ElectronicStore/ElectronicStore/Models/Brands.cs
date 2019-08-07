using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class Brands
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public string MetaTitle { get; set; }
        public long ParenId { get; set; }
        public int DisplayOrder { get; set; }
        public string SeoTitle { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public String ModifiedBy { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool Status { get; set; }
        public bool ShowOnHome { get; set; }
    }
}
