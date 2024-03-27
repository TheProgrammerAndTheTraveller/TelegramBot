using System;
using System.Collections.Generic;

namespace TelegramBot.DbFirst;

public partial class ClickCount
{
    public long ChatId { get; set; }

    public int ClickNumber { get; set; }
}
