using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecondHand.Models.Domain;
namespace SecondHand.DataAccess.Data
{

    public partial class S2HandDbContext : IdentityDbContext<ApplicationUser>
    {
        public S2HandDbContext()
        {
        }

        public S2HandDbContext(DbContextOptions<S2HandDbContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016; Initial Catalog = vule2303_SecondHandStore; User id = vule2303_SecondHandStore; Password = admin123; TrustServerCertificate=True");



        //    
        //    public virtual DbSet<Adress> Adresses { get; set; }

        //    public virtual DbSet<Brand> Brands { get; set; }

        //    public virtual DbSet<CartItem> CartItems { get; set; }

        //    public virtual DbSet<Category> Categories { get; set; }

        //    public virtual DbSet<CategoryDiscount> CategoryDiscounts { get; set; }

        //    public virtual DbSet<Contact> Contacts { get; set; }

        //    public virtual DbSet<Favorite> Favorites { get; set; }

        //    public virtual DbSet<Order> Orders { get; set; }

        //    public virtual DbSet<OrderItem> OrderItems { get; set; }

        //    public virtual DbSet<Product> Products { get; set; }

        //    public virtual DbSet<Promotion> Promotions { get; set; }

        //    public virtual DbSet<Role> Roles { get; set; }

        //    public virtual DbSet<User> Users { get; set; }

        //    public virtual DbSet<UserAddress> UserAddresses { get; set; }

        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016; Initial Catalog = vule2303_SecondHandStore; User id = vule2303_SecondHandStore; Password = admin123; TrustServerCertificate=True");

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{    

        //    modelBuilder.Entity<Adress>(entity =>
        //    {
        //        entity.HasKey(e => e.Id).HasName("pk_Adress");

        //        entity.ToTable("Adress");

        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.HasOne(d => d.AdressNavigation).WithMany(p => p.Adresses)
        //            .HasForeignKey(d => d.AdressId)
        //            .HasConstraintName("fk_Adress_UserAddress");

        //        entity.HasOne(d => d.User).WithMany(p => p.Adresses)
        //            .HasForeignKey(d => d.UserId)
        //            .HasConstraintName("fk_Adress_User");
        //    });

        //    modelBuilder.Entity<Brand>(entity =>
        //    {
        //        entity.HasKey(e => e.Id).HasName("pk_Brand");

        //        entity.ToTable("Brand");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //        entity.Property(e => e.Name).HasMaxLength(50);
        //    });

        //    modelBuilder.Entity<CartItem>(entity =>
        //    {
        //        entity.HasKey(e => new { e.UserId, e.ProductId });

        //        entity.ToTable("CartItem");

        //        entity.Property(e => e.Created).HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<Category>(entity =>
        //    {
        //        entity.ToTable("Category");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //        entity.Property(e => e.Created).HasColumnType("datetime");
        //        entity.Property(e => e.Modifield).HasColumnType("datetime");
        //        entity.Property(e => e.Name).HasMaxLength(50);

        //        entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
        //            .HasForeignKey(d => d.ParentId)
        //            .HasConstraintName("fk_Category_Category");
        //    });

        //    modelBuilder.Entity<CategoryDiscount>(entity =>
        //    {
        //        entity.ToTable("CategoryDiscount");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //        entity.Property(e => e.Code).HasMaxLength(50);
        //        entity.Property(e => e.Created).HasColumnType("datetime");
        //        entity.Property(e => e.DiscountType).HasMaxLength(50);
        //        entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 0)");
        //        entity.Property(e => e.EndDate).HasColumnType("datetime");
        //        entity.Property(e => e.Modified).HasColumnType("datetime");
        //        entity.Property(e => e.StartDate).HasColumnType("datetime");

        //        entity.HasOne(d => d.Category).WithMany(p => p.CategoryDiscounts)
        //            .HasForeignKey(d => d.CategoryId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("fk_CategoryDiscount_Category");
        //    });

        //    modelBuilder.Entity<Contact>(entity =>
        //    {
        //        entity.HasKey(e => e.Id).HasName("pk_Contact");

        //        entity.ToTable("Contact");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //        entity.Property(e => e.Email).HasMaxLength(50);
        //        entity.Property(e => e.FullName).HasMaxLength(255);
        //        entity.Property(e => e.Phone).HasMaxLength(20);
        //    });

        //    modelBuilder.Entity<Favorite>(entity =>
        //    {
        //        entity.HasKey(e => new { e.UserId, e.ProductId });

        //        entity.ToTable("Favorite");

        //        entity.Property(e => e.Created).HasColumnType("datetime");

        //        entity.HasOne(d => d.Product).WithMany(p => p.Favorites)
        //            .HasForeignKey(d => d.ProductId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("fk_Favorite_Product");

        //        entity.HasOne(d => d.User).WithMany(p => p.Favorites)
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("fk_Favorite_User");
        //    });

        //    modelBuilder.Entity<Order>(entity =>
        //    {
        //        entity.ToTable("Order");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //        entity.Property(e => e.Created).HasColumnType("datetime");
        //        entity.Property(e => e.Disscount).HasColumnType("decimal(18, 0)");
        //        entity.Property(e => e.OrderStatus).HasMaxLength(50);
        //        entity.Property(e => e.PaymentMethod).HasMaxLength(50);
        //        entity.Property(e => e.PaymentStatus).HasMaxLength(50);
        //        entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 0)");
        //        entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

        //        entity.HasOne(d => d.Promotion).WithMany(p => p.Orders)
        //            .HasForeignKey(d => d.PromotionId)
        //            .HasConstraintName("fk_Order_Promotion_0");

        //        entity.HasOne(d => d.User).WithMany(p => p.Orders)
        //            .HasForeignKey(d => d.UserId)
        //            .HasConstraintName("fk_Order_User");
        //    });

        //    modelBuilder.Entity<OrderItem>(entity =>
        //    {
        //        entity.ToTable("OrderItem");

        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
        //            .HasForeignKey(d => d.OrderId)
        //            .HasConstraintName("fk_OrderItem_Order");

        //        entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
        //            .HasForeignKey(d => d.ProductId)
        //            .HasConstraintName("fk_OrderItem_Product");
        //    });

        //    modelBuilder.Entity<Product>(entity =>
        //    {
        //        entity.ToTable("Product");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //        entity.Property(e => e.Color).HasMaxLength(50);
        //        entity.Property(e => e.Conditon).HasMaxLength(50);
        //        entity.Property(e => e.Created).HasColumnType("datetime");
        //        entity.Property(e => e.Defects).HasMaxLength(50);
        //        entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        //        entity.Property(e => e.Size).HasMaxLength(20);

        //        entity.HasOne(d => d.Brand).WithMany(p => p.Products)
        //            .HasForeignKey(d => d.BrandId)
        //            .HasConstraintName("fk_Product_Brand");

        //        entity.HasOne(d => d.Category).WithMany(p => p.Products)
        //            .HasForeignKey(d => d.CategoryId)
        //            .HasConstraintName("fk_Product_Category");
        //    });

        //    modelBuilder.Entity<Promotion>(entity =>
        //    {
        //        entity.HasKey(e => e.Id).HasName("pk_Promotion_0");

        //        entity.ToTable("Promotion");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //        entity.Property(e => e.Code).HasMaxLength(50);
        //        entity.Property(e => e.Created).HasColumnType("datetime");
        //        entity.Property(e => e.DiscountType).HasMaxLength(50);
        //        entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 0)");
        //        entity.Property(e => e.EndDate).HasColumnType("datetime");
        //        entity.Property(e => e.Modifield).HasColumnType("datetime");
        //        entity.Property(e => e.StartDate).HasColumnType("datetime");
        //    });

        //    modelBuilder.Entity<UserAddress>(entity =>
        //    {
        //        entity.ToTable("UserAddress");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //        entity.Property(e => e.Created).HasColumnType("datetime");
        //        entity.Property(e => e.Modifiled).HasColumnType("datetime");
        //        entity.Property(e => e.Name).HasMaxLength(255);
        //        entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
