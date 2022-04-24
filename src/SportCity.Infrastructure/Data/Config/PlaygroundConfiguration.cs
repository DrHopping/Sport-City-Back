using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportCity.Core.Entities.PlaygroundAggregate;

namespace SportCity.Infrastructure.Data.Config;

public class PlaygroundConfiguration : IEntityTypeConfiguration<Playground>
{
  public void Configure(EntityTypeBuilder<Playground> builder)
  {
    builder.Metadata.FindNavigation(nameof(Playground.Events))?.SetPropertyAccessMode(PropertyAccessMode.Field);;
    builder.Metadata.FindNavigation(nameof(Playground.Reviews))?.SetPropertyAccessMode(PropertyAccessMode.Field);;
    builder.OwnsOne(o => o.Location, l =>
    {
      l.WithOwner();
    });
  }
}
