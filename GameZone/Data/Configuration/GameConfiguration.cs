using GameZone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameZone.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure (EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).HasMaxLength(255).IsRequired();
            builder.Property(g => g.Cover).HasMaxLength(255).IsRequired();
            builder.Property(g => g.Description).HasMaxLength(2500).IsRequired();
            builder.HasOne(g => g.Category).WithMany(c => c.Games).HasForeignKey(g => g.CategoryId);
            builder.HasMany<Device>().WithMany().UsingEntity<GameDevice>();
        }
    }
}
