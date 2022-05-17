using Microsoft.EntityFrameworkCore;
using SportCity.Core.Entities.CategoryAggregate;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.SportAggregate;

namespace SportCity.Infrastructure.Data;

public static class AppContextSeed
{
  public static async Task SeedAsync(this AppDbContext context)
  {
    if (context.Database.IsSqlServer())
    {
      await context.Database.MigrateAsync();
    }
    
    if (!await context.Categories.AnyAsync())
    {
      await context.Categories.AddRangeAsync(GetPreconfiguredCategories());
      await context.SaveChangesAsync();
    }
    
    if (!await context.Sports.AnyAsync())
    {
      await context.Sports.AddRangeAsync(GetPreconfiguredSports());
      await context.SaveChangesAsync();
    }
    
    if (!await context.Cities.AnyAsync())
    {
      await context.Cities.AddRangeAsync(GetPreconfiguredCities());
      await context.SaveChangesAsync();
    }
  }
  
  static IEnumerable<Category> GetPreconfiguredCategories()
  {
    return new List<Category>
    {
      new("Amateur"),
      new("Beginner"),
      new("Professional")
    };
  }
  
  static IEnumerable<Sport> GetPreconfiguredSports()
  {
    return new List<Sport>
    {
      new("Football"),
      new("Volleyball"),
      new("Basketball")
    };
  }
  
  static IEnumerable<City> GetPreconfiguredCities()
  {
    return new List<City>
    {
      new("Kyiv"),
      new("Lviv"),
      new("Vinnitsya")
    };
  }
  
}
