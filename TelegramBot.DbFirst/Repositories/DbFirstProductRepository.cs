using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Domain;
using TelegramBot.Domain.Repositories;

namespace TelegramBot.DbFirst.Repositories
{
    public class DbFirstProductRepository : IProductRepository
    {
        public readonly TelegramBotContext _telegramBotDBContext;

        public DbFirstProductRepository(TelegramBotContext telegramBotDBContext)
        {
            _telegramBotDBContext = telegramBotDBContext;
        }

        public async Task<int> Add(Domain.Product catalog)
        {
            await _telegramBotDBContext.Products.AddAsync(ToDb(catalog));
            await _telegramBotDBContext.SaveChangesAsync();

            return catalog.Id;
        }

        private static Domain.Product ToDomain(Product catalog)
        {
            return new Domain.Product()
            {
                CategoryId = catalog.CategoryId,
                Name = catalog.Name,
                Description = catalog.Description,
                Category = catalog.Category == null ? null : new() { Id = catalog.Category.Id, Name = catalog.Category.Name },
                Id = catalog.Id,
                OrderDetails = catalog.OrderDetails?.Select(x => new Domain.OrderDetails()
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Status = new() { Id = x.Status.Id, Name = x.Status.Name, Description = x.Status.Description }
                }).ToList() ?? [],
                Price = catalog.Price,
                Attributes = catalog.ProductAttributes?.Select(x => new Domain.ProductAttribute
                {
                    Attribute = new Domain.Attribute() { Id = x.Attribute.Id, Name = x.Attribute.Name },
                    AttributeId = x.Attribute.Id,
                    Id = x.Attribute.Id,
                    ProductId = x.ProductId,
                    Value = x.Value,
                }).ToList() ?? []
            };
        }

        private static Product ToDb(Domain.Product catalog)
        {
            return new Product()
            {
                CategoryId = catalog.CategoryId,
                Name = catalog.Name,
                Description = catalog.Description,
                Category = catalog.Category == null ? null : new() { Id = catalog.Category.Id, Name = catalog.Category.Name },
                Id = catalog.Id,
                OrderDetails = catalog.OrderDetails?.Select(x => new OrderDetail()
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Status = new() { Id = x.Status.Id, Name = x.Status.Name, Description = x.Status.Description }
                }).ToList() ?? [],
                Price = catalog.Price,
                ProductAttributes = catalog.Attributes?.Select(x => new ProductAttribute
                {
                    Attribute = new Attribute() { Id = x.Attribute.Id, Name = x.Attribute.Name },
                    AttributeId = x.Attribute.Id,
                    Id = x.Attribute.Id,
                    ProductId = x.ProductId,
                    Value = x.Value,
                }).ToList() ?? []
            };
        }


        public async Task<IEnumerable<Domain.Product>> GetAll()
        {
            var catalog = await _telegramBotDBContext.Products
                .Include(e => e.ProductAttributes)
                .ThenInclude(e => e.Attribute)
                .Select(e => ToDomain(e))
                .ToArrayAsync();

            return catalog;
        }
    }
}
