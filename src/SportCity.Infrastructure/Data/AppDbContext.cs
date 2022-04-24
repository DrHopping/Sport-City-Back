using Ardalis.EFCore.Extensions;
using SportCity.Core.ProjectAggregate;
using SportCity.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

  //public AppDbContext(DbContextOptions options) : base(options)
  //{
  //}

  public AppDbContext(DbContextOptions<AppDbContext> options, IMediator? mediator)
      : base(options)
  {
    _mediator = mediator;
  }

  public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
  public DbSet<Project> Projects => Set<Project>();

  public DbSet<Category> Categories => Set<Category>();
  public DbSet<City> Cities => Set<City>();
  public DbSet<Event> Events => Set<Event>();
  public DbSet<Player> Players => Set<Player>();
  public DbSet<Playground> Playgrounds => Set<Playground>();
  public DbSet<Review> Feedbacks => Set<Review>();
  public DbSet<Sport> Sports => Set<Sport>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

    // alternately this is built-in to EF Core 2.2
    //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_mediator == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
        .Select(e => e.Entity)
        .Where(e => e.Events.Any())
        .ToArray();

    foreach (var entity in entitiesWithEvents)
    {
      var events = entity.Events.ToArray();
      entity.Events.Clear();
      foreach (var domainEvent in events)
      {
        await _mediator.Publish(domainEvent).ConfigureAwait(false);
      }
    }

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
