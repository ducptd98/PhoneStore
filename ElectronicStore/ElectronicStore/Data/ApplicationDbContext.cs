using System;
using System.Collections.Generic;
using System.Text;
using ElectronicStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductAttributes> ProductAttributes { get; set; }
        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
