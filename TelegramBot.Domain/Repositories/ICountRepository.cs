using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Domain.Repositories
{
    public interface ICountRepository
    {
        Task<int> GetCount(long chatID);
    }
}
