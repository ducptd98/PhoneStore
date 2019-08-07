using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class Appointments
    {
        public long Id { get; set; }
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUsers Customer { get; set; }
        public DateTimeOffset LatestUpdatedOn { get; set; }
        public string ShipperId { get; set; }
        [ForeignKey("ShipperId")]
        public ApplicationUsers LatestUpdatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public string AdminId { get; set; }
        [ForeignKey("AdminId")]
        public ApplicationUsers CreatedBy { get; set; }

        public int Status { get; set; }

    }
}
