using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Domain;

namespace TelegramBot.Data
{
    public class AppDbContext : DbContext
    {   
        public DbSet<Catalog> Catalog {  get; set; }
        public DbSet<ClickCount> ClickCounts { get; set; }
 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

