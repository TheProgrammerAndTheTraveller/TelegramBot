using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.DbFirst;
using TelegramBot.Domain;
using TelegramBot.Domain.Repositories;

namespace TelegramBot.DbFirst.Repositories
{

    public class DBFirstCountRepository : ICountRepository
    {
        public readonly TelegramBotContext _telegramBotDBContext;


        public DBFirstCountRepository(TelegramBotContext telegramBotDBContext)
        {
            _telegramBotDBContext = telegramBotDBContext;
        }
        public async Task<int> GetCount(long chatId)
        {
            var count = await _telegramBotDBContext.ClickCounts.FirstOrDefaultAsync(e => e.ChatId == chatId);

            if (count == null)
            {
                count = new ClickCount() { ChatId = chatId, ClickNumber = 1 };
                await _telegramBotDBContext.ClickCounts.AddAsync(count);
            }
            else
            {
                count.ClickNumber++;
                //увеличить count на 1, вернуть count
            }

            await _telegramBotDBContext.SaveChangesAsync();

            return count.ClickNumber;
        }
    }
}
