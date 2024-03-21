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

        public async Task<IEnumerable<Catalog>> GetAll()
        {
            var catalog = await _appDbContext.Catalog.ToArrayAsync();
            return catalog;
        }
    }
}
