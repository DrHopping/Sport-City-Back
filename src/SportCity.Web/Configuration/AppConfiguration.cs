using Microsoft.AspNetCore.Identity;
using SportCity.Infrastructure.Data;
using SportCity.Infrastructure.Identity;
using SportCity.Web.Middleware;

namespace SportCity.Web.Configuration;

public static class AppConfiguration
{
  public static async Task SeedAppDb(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var scopedProvider = scope.ServiceProvider;
    try
    {
      var appContext = scopedProvider.GetRequiredService<AppDbContext>();
      await appContext.Database.EnsureCreatedAsync();
      await appContext.SeedAsync();
    }
    catch (Exception ex)
    {
      app.Logger.LogError(ex, "An error occurred setting the DB.");
    }
  }

  public static async Task SeedIdentityDb(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var scopedProvider = scope.ServiceProvider;
    try
    {
      var userManager = scopedProvider.GetRequiredService<UserManager<EfApplicationUser>>();
      var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
      var identityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
      await identityContext.Database.EnsureCreatedAsync();
      await identityContext.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
      app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
  }

  public static void UseApplicationMiddleware(this WebApplication app)
  {
    app.UseMiddleware<ExceptionHandlerMiddleware>();
    app.UseMiddleware<TokenHandlerMiddleware>();
    app.UseMiddleware<OwningUserAccessMiddleware>();
  }

}
