using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Domain
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public virtual List<ProductAttribute> Attributes { get; set; } = null!;

        public virtual Categories Category { get; set; } = null!;
    }
}
