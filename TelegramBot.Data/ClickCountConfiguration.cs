using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Domain;

namespace TelegramBot.Data
{
    public class ClickCountConfiguration : IEntityTypeConfiguration<ClickCount>
    {
        public void Configure(EntityTypeBuilder<ClickCount> builder)
        {
            builder.HasKey(u => u.ChatId);
        }
    }
}
