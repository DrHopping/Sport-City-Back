using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportCity.Core.Entities.PlayerAggregate;
using SportCity.Infrastructure.Data;
using SportCity.SharedKernel;

namespace SportCity.Infrastructure.Identity;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedAsync(
        this AppIdentityDbContext identityDbContext,
        AppDbContext context,
        UserManager<EfApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (identityDbContext.Database.IsSqlServer())
        {
            await identityDbContext.Database.MigrateAsync();
        }

        if (await identityDbContext.Users.AnyAsync()) return;

        await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        await roleManager.CreateAsync(new IdentityRole(Roles.User));

        var admin = new EfApplicationUser { UserName = "admin@microsoft.com", Email = "admin@microsoft.com" };
        var result = await userManager.CreateAsync(admin, "Admin123!");
        admin = await userManager.FindByEmailAsync(admin.Email);
        await userManager.AddToRoleAsync(admin, Roles.Admin);

        await context.Players.AddAsync(new Player(admin.Id, "System", "Admin", 1));
        await context.SaveChangesAsync();
    }
}
