using TelegramBot.Domain;

namespace TelegramBot.Domain.Repositories
{
    public interface ICatalogRepository
    {
        Task<IEnumerable<Catalog>> GetAll();
    }
}