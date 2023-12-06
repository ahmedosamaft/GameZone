using GameZone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameZone.Data.Configuration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure (EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).HasMaxLength(255).IsRequired();
            builder.Property(g => g.Icon).HasMaxLength(512).IsRequired();
        }
    }
}
