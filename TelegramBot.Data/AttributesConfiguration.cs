using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramBot.Domain;
using Attribute = TelegramBot.Domain.Attribute;

namespace TelegramBot.Data
{
    public class AttributesConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).UseIdentityAlwaysColumn();
            builder.HasMany<ProductAttribute>()
                .WithOne(u => u.Attribute)
                .HasForeignKey(u => u.AttributeId);

            builder.ToTable("Attributes");
        }
    }
}
