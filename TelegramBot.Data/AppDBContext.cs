using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TelegramBot.Domain;

namespace TelegramBot.Data
{
    public class AppDbContext : DbContext
    {   
        public DbSet<Product> Catalog {  get; set; }
        public DbSet<ClickCount> ClickCounts { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Domain.Attribute> Attributes { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

