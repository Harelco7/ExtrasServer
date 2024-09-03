using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CLExtras2.Models;

public partial class Extras2Context : DbContext
{
    public Extras2Context()
    {
    }

    public Extras2Context(DbContextOptions<Extras2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Box> Boxes { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();
        String connStr = config.GetConnectionString("DefaultConnectionString");
        optionsBuilder.UseSqlServer(connStr);
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Box>(entity =>
        {
            entity.HasKey(e => e.BoxId).HasName("PK__Boxes__24790DCE679F1EBE");

            entity.Property(e => e.BoxId).HasColumnName("box_id");
            entity.Property(e => e.AlergicType)
                .HasMaxLength(100)
                .HasColumnName("alergic_type");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("box_description");
            entity.Property(e => e.BoxImage).HasColumnName("box_image");
            entity.Property(e => e.BoxName)
                .HasMaxLength(255)
                .HasColumnName("box_name");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.DateAdded)
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.QuantityAvailable).HasColumnName("quantity_available");

            entity.HasOne(d => d.Business).WithMany(p => p.Boxes)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK__Boxes__business___47DBAE45");

            entity.HasOne(d => d.Order).WithMany(p => p.Boxes)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Boxes__order_id__48CFD27E");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.BusinessId).HasName("PK__Business__DC0DC16E9F30E3C2");

            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(255)
                .HasColumnName("business_name");
            entity.Property(e => e.BusinessType)
                .HasMaxLength(255)
                .HasColumnName("business_type");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(255)
                .HasColumnName("contact_info");
            entity.Property(e => e.DailySalesHour).HasMaxLength(255);
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.OpeningHours).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Businesse__user___3C69FB99");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB85FD4E4EA6");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customer_name");
            entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Favorite).WithMany(p => p.Customers)
                .HasForeignKey(d => d.FavoriteId)
                .HasConstraintName("FK__Customers__favor__4222D4EF");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Customers__user___412EB0B6");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__46ACF4CB2BEA53ED");

            entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");
            entity.Property(e => e.BusinessId)
                .HasMaxLength(255)
                .HasColumnName("business_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Favorites__custo__49C3F6B7");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__0BBF6EE68775DA1C");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.MessageName)
                .HasMaxLength(300)
                .HasColumnName("message_name");
            entity.Property(e => e.MessageText)
                .HasMaxLength(500)
                .HasColumnName("message_text");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__4659622942956127");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderRaiting).HasColumnName("order_raiting");
            entity.Property(e => e.BusinessID).HasColumnName("BusinessID");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(100)
                .HasColumnName("order_status");
            entity.Property(e => e.QuantityOrdered).HasColumnName("quantity_ordered");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.BoxID).HasColumnName("box_id");
            entity.Property(e => e.boxDescription).HasColumnName("boxDescription");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__customer__44FF419A");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__47027DF5EC50F854");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.AlergicType)
                .HasMaxLength(100)
                .HasColumnName("alergic_type");
            entity.Property(e => e.ProductDescription)
                .HasColumnType("text")
                .HasColumnName("product_description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");

            entity.HasMany(d => d.Boxes).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "Contain",
                    r => r.HasOne<Box>().WithMany()
                        .HasForeignKey("BoxId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Contain__box_id__4F7CD00D"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Contain__product__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("ProductId", "BoxId").HasName("PK__Contain__B545ED29BEF3B5CD");
                        j.ToTable("Contain");
                        j.IndexerProperty<int>("ProductId").HasColumnName("product_id");
                        j.IndexerProperty<int>("BoxId").HasColumnName("box_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F650AEF5E");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .HasColumnName("email");
            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_type");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Message).WithMany(p => p.Users)
                .HasForeignKey(d => d.MessageId)
                .HasConstraintName("FK__Users__message_i__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<User>()
        .Property(u => u.UserId)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Customer>()
       .Property(u => u.CustomerId)
       .ValueGeneratedOnAdd();

        modelBuilder.Entity<Business>()
       .Property(u => u.BusinessId)
       .ValueGeneratedOnAdd();


        modelBuilder.Entity<Box>()
        .Property(u => u.BoxId)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Favorite>()
      .Property(u => u.FavoriteId)
      .ValueGeneratedOnAdd();

        modelBuilder.Entity<Message>()
        .Property(u => u.MessageId)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>()
        .Property(u => u.OrderId)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>()
        .Property(u => u.ProductId)
        .ValueGeneratedOnAdd();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
