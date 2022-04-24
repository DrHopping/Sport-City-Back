using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportCity.Core.Entities.PlayerAggregate;

namespace SportCity.Infrastructure.Data.Config;

public class PlayerConfiguration : IEntityTypeConfiguration<Player> 
{
  public void Configure(EntityTypeBuilder<Player> builder)
  {
    builder.HasIndex(p => p.IdentityGuid).IsUnique();
  }
}
