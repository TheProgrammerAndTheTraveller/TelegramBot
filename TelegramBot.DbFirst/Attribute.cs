using System;
using System.Collections.Generic;

namespace TelegramBot.DbFirst;

public partial class Attribute
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
}
