using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportCity.Core.Entities.PlayerAggregate;

namespace SportCity.Infrastructure.Data.Config;

public class PlayerConfiguration : IEntityTypeConfiguration<Player> 
{
  public void Configure(EntityTypeBuilder<Player> builder)
  {
    builder.Metadata.FindNavigation(nameof(Player.ParticipatedEvents))?.SetPropertyAccessMode(PropertyAccessMode.Field);;
    builder.Metadata.FindNavigation(nameof(Player.OrganizedEvents))?.SetPropertyAccessMode(PropertyAccessMode.Field);;
    builder.HasIndex(p => p.IdentityGuid).IsUnique();
    builder.HasMany(p => p.ParticipatedEvents).WithMany(e => e.Participants);
  }
}
