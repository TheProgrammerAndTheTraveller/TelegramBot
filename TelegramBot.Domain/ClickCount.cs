using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Domain
{
    public class ClickCount
    {
        public long ChatId { get; set; }
        public int ClickNumber { get; set; }
    }
}
