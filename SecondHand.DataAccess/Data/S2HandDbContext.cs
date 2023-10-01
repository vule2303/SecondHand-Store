using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecondHand.Models.Domain;
using SecondHand.Models.Models.Domain;

namespace SecondHand.DataAccess.Data
{

    public partial class S2HandDbContext : IdentityDbContext<ApplicationUser>
    {
        public S2HandDbContext()
        {
        }
        public virtual DbSet<Adress> Adresses { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<CartItem> CartItems { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<CategoryDiscount> CategoryDiscounts { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Favorite> Favorites { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetail { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Promotion> Promotions { get; set; }

        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public S2HandDbContext(DbContextOptions<S2HandDbContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016; Initial Catalog = vule2303_SecondHandStore; User id = vule2303_SecondHandStore; Password = admin123; TrustServerCertificate=True");


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }




        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
