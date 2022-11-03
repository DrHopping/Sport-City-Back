using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SportCity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportCity.Infrastructure;

public static class StartupSetup
{
    public static void AddDbContext<T>(this IServiceCollection services, string connectionString) where T : DbContext =>
        services.AddDbContext<T>(options => options.UseSqlite(connectionString));
}
