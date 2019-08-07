using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicStore.Models
{
    public class ApplicationUsers:IdentityUser
    {
        [Required]
        [Display(Name ="Full name")]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string ShippingAddress { get; set; }
        public string CardNumber { get; set; }
        public bool IsSupperAdmin { get; set; }

    }
}
