using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class Slides
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string DisplayOrder { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public String ModifiedBy { get; set; }
        public bool Status { get; set; }
    }
}
