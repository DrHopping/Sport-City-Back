using Ardalis.EFCore.Extensions;
using SportCity.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.EventAggregate;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;
using SportCity.Core.Entities.SportAggregate;

namespace SportCity.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IMediator? _mediator;

  public AppDbContext(DbContextOptions<AppDbContext> options, IMediator? mediator)
      : base(options)
  {
    _mediator = mediator;
  }
  public DbSet<Category> Categories => Set<Category>();
  public DbSet<City> Cities => Set<City>();
  public DbSet<Event> Events => Set<Event>();
  public DbSet<Player> Players => Set<Player>();
  public DbSet<Playground> Playgrounds => Set<Playground>();
  public DbSet<Review> Reviews => Set<Review>();
  public DbSet<Sport> Sports => Set<Sport>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    await PublishEvents(cancellationToken);
    return result;
  }

  private async Task PublishEvents(CancellationToken cancellationToken)
  {
      // ignore events if no dispatcher provided
      if (_mediator == null) return;

      // dispatch events only if save was successful
      var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
          .Select(e => e.Entity)
          .Where(e => e.GetDomainEvents().Any())
          .ToArray();

      foreach (var entity in entitiesWithEvents)
      {
          var events = entity.GetDomainEvents().ToArray();
          entity.GetDomainEvents().Clear();
          foreach (var domainEvent in events)
          {
              await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
          }
      }
  }


  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
