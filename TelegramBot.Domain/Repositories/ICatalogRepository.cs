using System.ComponentModel;
using TelegramBot.Domain;

namespace TelegramBot.Domain.Repositories
{
    public interface ICatalogRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<int> Add(Product catalog);
    }
}