using System;
using System.Collections.Generic;

namespace TelegramBot.DbFirst;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int Quantity { get; set; }

    public int ProductId { get; set; }

    public int StatusId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
