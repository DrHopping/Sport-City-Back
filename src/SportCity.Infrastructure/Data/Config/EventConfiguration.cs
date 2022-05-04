using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportCity.Core.Entities.EventAggregate;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;

namespace SportCity.Infrastructure.Data.Config;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
  public void Configure(EntityTypeBuilder<Event> builder)
  {
    builder.Metadata.FindNavigation(nameof(Event.Participants))?.SetPropertyAccessMode(PropertyAccessMode.Field);
    builder.HasOne(e => e.Organizer).WithMany(o => o.OrganizedEvents);
    builder.HasMany(e => e.Participants).WithMany(p => p.ParticipatedEvents).UsingEntity(j => j.ToTable("EventParticipants"));
  }
}
