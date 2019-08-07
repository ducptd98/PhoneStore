using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ElectronicStore.Models
{
    public class Order
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }

        public long AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual Appointments Appointments { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal feeShip { get; set; }
        public decimal Discount { get; set; }
    }
}
