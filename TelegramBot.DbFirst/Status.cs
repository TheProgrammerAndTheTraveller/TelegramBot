using System;
using System.Collections.Generic;

namespace TelegramBot.DbFirst;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
