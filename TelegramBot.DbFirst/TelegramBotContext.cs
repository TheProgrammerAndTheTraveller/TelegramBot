using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TelegramBot.DbFirst;

public partial class TelegramBotContext : DbContext
{
    public TelegramBotContext()
    {
    }

    public TelegramBotContext(DbContextOptions<TelegramBotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ClickCount> ClickCounts { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:TelegramDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<ClickCount>(entity =>
        {
            entity.HasKey(e => e.ChatId);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_OrderDetails_ProductId");

            entity.HasIndex(e => e.StatusId, "IX_OrderDetails_StatusID");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Status).WithMany(p => p.OrderDetails).HasForeignKey(d => d.StatusId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CategoryId).HasDefaultValue(0);

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasIndex(e => e.AttributeId, "IX_ProductAttributes_AttributeId");

            entity.HasIndex(e => e.ProductId, "IX_ProductAttributes_ProductId");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Attribute).WithMany(p => p.ProductAttributes).HasForeignKey(d => d.AttributeId);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributes).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
