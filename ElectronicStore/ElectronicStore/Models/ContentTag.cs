using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class ContentTag
    {
        public long Id { get; set; }
        [Display(Name ="Content")]
        public long ContentId { get; set; }
        [ForeignKey("ContentId")]
        public virtual Contents Contents { get; set; }
        [Display(Name ="Tag")]
        public long TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tags Tags { get; set; }
    }
}
