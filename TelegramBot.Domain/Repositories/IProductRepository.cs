using System.ComponentModel;
using TelegramBot.Domain;

namespace TelegramBot.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<int> Add(Product catalog);
    }
}