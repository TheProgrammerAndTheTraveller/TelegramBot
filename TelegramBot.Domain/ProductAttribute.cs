namespace TelegramBot.Domain
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public int ProductId {  get; set; }
        public int AttributeId {  get; set; }
        public string Value {  get; set; }
        public Attribute Attribute { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
