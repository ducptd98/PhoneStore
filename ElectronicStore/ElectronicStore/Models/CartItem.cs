using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class CartItem
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }

    }
}
