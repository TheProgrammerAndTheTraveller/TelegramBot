using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Domain
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId {  get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity {  get; set; }
        public int ProductId {  get; set; }
        public int StatusID {  get; set; }
        public virtual Status Status { get; set; } = null!;
        public Product Product { get; set; } = new();
        
    }
}
