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

    public class CountRepository : ICountRepository
    {
        public readonly AppDbContext _appDbContext;

        public CountRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> GetCount(long chatId)
        {
            var count = await _appDbContext.ClickCounts.FirstOrDefaultAsync(e => e.ChatId == chatId);

            if (count == null)
            {
                count = new ClickCount() { ChatId = chatId, ClickNumber = 1 };
                await _appDbContext.ClickCounts.AddAsync(count);
            } 
            else
            {
                count.ClickNumber++;
                //увеличить count на 1, вернуть count
            }

            await _appDbContext.SaveChangesAsync();

            return count.ClickNumber;
        }
    }
}
