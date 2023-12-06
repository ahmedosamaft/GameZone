using GameZone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameZone.Data.Configuration
{
    public class GameDeviceConfiguration : IEntityTypeConfiguration<GameDevice>
    {
        public void Configure (EntityTypeBuilder<GameDevice> builder)
        {
            builder.HasKey(g => new { g.DeviceId, g.GameId });
        }
    }
}
