using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        
        public string ImagePath { get; set; }
        public string Title { get; set; }

        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}
