using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Domain;
using TelegramBot.Domain.Repositories;

namespace TelegramBot.Data.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        public readonly AppDbContext _appDbContext;

        public CatalogRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //реализуй!
        public async Task<int> Add(Product catalog)
        {
            await _appDbContext.Catalog.AddAsync(catalog);
            await _appDbContext.SaveChangesAsync();
            return catalog.Id;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var catalog = await _appDbContext.Catalog
                .Include(e => e.Attributes)
                .ThenInclude(e => e.Attribute)
                .ToArrayAsync();

            return catalog;
        }
    }
}
