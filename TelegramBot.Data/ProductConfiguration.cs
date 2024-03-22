using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Domain;

namespace TelegramBot.Data
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).UseIdentityAlwaysColumn();
            builder.HasMany(u => u.Attributes)
                .WithOne(u => u.Product)
                .HasForeignKey(u => u.ProductId);

            builder.ToTable("Products");
        }
    }
}
