using Microsoft.EntityFrameworkCore;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.EventAggregate;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;
using SportCity.Core.Entities.SportAggregate;
using SportCity.SharedKernel;

namespace SportCity.Infrastructure.Data;

public static class AppContextSeed
{
    public static async Task SeedAsync(this AppDbContext context)
    {
        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync();
        }

        await SeedData(context.Categories, GetSeedCategories(), context);
        await SeedData(context.Sports, GetSeedSports(), context);
        await SeedData(context.Cities, GetSeedCities(), context);
        await SeedData(context.Players, GetSeedPlayers(), context);
        await SeedData(context.Playgrounds, GetSeedPlaygrounds(), context);
        await SeedData(context.Reviews, GetSeedReviews(), context);
        await SeedData(context.Events, GetSeedEvents(), context);
        await SeedParticipants(context);

        await context.SaveChangesAsync();
    }


    private static async Task SeedData<T>(DbSet<T> dbSet, IEnumerable<T> data, AppDbContext context) where T: BaseEntity
    {
        if (await dbSet.AnyAsync()) return;
        await dbSet.AddRangeAsync(data);
        await context.SaveChangesAsync();
    }

    private static async Task SeedParticipants(AppDbContext context)
    {
        if (await context.Events.AnyAsync(@event => @event.Participants.Any())) return;

        var players = await context.Players.ToListAsync();
        var events = await context.Events.ToListAsync();

        foreach (var @event in events)
        {
            var count = Random.Shared.Next(0, @event.Capacity + 1);
            for (int i = 0; i < count; i++)
            {
                @event.AddParticipant(players[i]);
            }
        }

        await context.SaveChangesAsync();
    }

    static IEnumerable<Category> GetSeedCategories()
    {
        return new List<Category> { new("Beginner"), new("Amateur"), new("Professional") };
    }

    static IEnumerable<Sport> GetSeedSports()
    {
        return new List<Sport> { new("Football"), new("Volleyball"), new("Basketball") };
    }

    static IEnumerable<City> GetSeedCities()
    {
        return new List<City> { new("Kyiv"), new("Vinnitsya") };
    }

    static IEnumerable<Player> GetSeedPlayers()
    {
        return Enumerable.Range(0, 10).Select(_ =>
            new Player(
                identityGuid: Guid.NewGuid().ToString(),
                firstName: Faker.Name.First(),
                lastName: Faker.Name.Last(),
                category: Random.Shared.Next(1, 4)));
    }

    static IEnumerable<Playground> GetSeedPlaygrounds()
    {
        return new List<Playground>
        {
            new(
                name: "Playground #1 V",
                description: "Playground number 1",
                city: 2,
                photoUrl:
                "https://daily.vn.ua/wp-content/uploads/2021/02/%D1%84%D1%83%D1%82%D0%B1%D0%BE%D0%BB%D1%8C%D1%87%D1%94%D0%B3.jpg",
                location: new Location("Хмельницьке шосе, 22", 49.23540664795841, 28.440391222955196)
            ),
            new(
                name: "Винницкий лицей №18 V",
                description: "Playground number 2",
                city: 2,
                photoUrl: "https://i.imgur.com/mQpDLhl.jpeg",
                location: new Location("вулиця Келецька, 97", 49.22584899174668, 28.405592560043473)
            ),
            new(
                name: "Школа №26 V",
                description: "Playground number 3",
                city: 2,
                photoUrl: "https://i.imgur.com/v9mRMBX.jpeg",
                location: new Location("Хмельницьке шосе, 27", 49.23504339192755, 28.438102690728698)
            ),
            new(
                name: "Школа №21 K",
                description: "Playground number 4",
                city: 1,
                photoUrl: "https://i.imgur.com/v9mRMBX.jpeg",
                location: new Location("Хмельницьке шосе, 27", 49.23504339192755, 28.438102690728698)
            ),
            new(
                name: "Киевский лицей №18 K",
                description: "Playground number 5",
                city: 1,
                photoUrl: "https://i.imgur.com/mQpDLhl.jpeg",
                location: new Location("вулиця Келецька, 97", 49.22584899174668, 28.405592560043473)
            ),
            new(
                name: "Школа №26 K",
                description: "Playground number 6",
                city: 1,
                photoUrl: "https://i.imgur.com/v9mRMBX.jpeg",
                location: new Location("Хмельницьке шосе, 27", 49.23504339192755, 28.438102690728698)
            ),
        };
    }

    static IEnumerable<Review> GetSeedReviews()
    {
        return new List<Review>
        {
            new(
                rating: 3.5,
                comment:
                "Very cool playground. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                reviewer: 1,
                playground: 1
            ),
            new(
                rating: 3.5,
                comment:
                "Very cool playground.",
                reviewer: 2,
                playground: 1
            ),
            new(
                rating: 3.5,
                comment:
                "Nice playground.",
                reviewer: 3,
                playground: 1
            ),
            new(
                rating: 3.5,
                comment:
                "Very cool playground. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                reviewer: 4,
                playground: 1
            ),
        };
    }

    static IEnumerable<Event> GetSeedEvents()
    {
        return new List<Event>
        {
            new(
                playgroundId: 1,
                categoryId: 1,
                sportId: 1,
                capacity: 10,
                dateTime: DateTime.UtcNow +
                          TimeSpan.FromDays(Random.Shared.Next(1, 30)) +
                          TimeSpan.FromMinutes(Random.Shared.Next(1, 1200)),
                organizerId: 1
            ),
            new(
                playgroundId: 2,
                categoryId: 2,
                sportId: 2,
                capacity: 2,
                dateTime: DateTime.UtcNow +
                          TimeSpan.FromDays(Random.Shared.Next(1, 30)) +
                          TimeSpan.FromMinutes(Random.Shared.Next(1, 1200)),
                organizerId: 2
            ),
            new(
                playgroundId: 3,
                categoryId: 3,
                sportId: 3,
                capacity: 6,
                dateTime: DateTime.UtcNow +
                          TimeSpan.FromDays(Random.Shared.Next(1, 30)) +
                          TimeSpan.FromMinutes(Random.Shared.Next(1, 1200)),
                organizerId: 3
            ),
            new(
                playgroundId: 4,
                categoryId: 2,
                sportId: 1,
                capacity: 4,
                dateTime: DateTime.UtcNow +
                          TimeSpan.FromDays(Random.Shared.Next(5, 30)) +
                          TimeSpan.FromMinutes(Random.Shared.Next(1, 1200)),
                organizerId: 1
            ),
            new(
                playgroundId: 5,
                categoryId: 1,
                sportId: 3,
                capacity: 6,
                dateTime: DateTime.UtcNow -
                          TimeSpan.FromDays(Random.Shared.Next(5, 30)) +
                          TimeSpan.FromMinutes(Random.Shared.Next(1, 1200)),
                organizerId: 1
            ),
            new(
                playgroundId: 6,
                categoryId: 2,
                sportId: 3,
                capacity: 2,
                dateTime: DateTime.UtcNow -
                          TimeSpan.FromDays(Random.Shared.Next(5, 30)) +
                          TimeSpan.FromMinutes(Random.Shared.Next(1, 1200)),
                organizerId: 1
            ),
        };
    }

}
