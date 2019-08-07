using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public int DisplayOrder { get; set; }
        public string Target { get; set; }
        public bool Status { get; set; }
        [Display(Name ="Menu Type")]
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual MenuTypes Types { get; set; }
    }
}
