using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using SportCity.Core;
using SportCity.Infrastructure;
using SportCity.Infrastructure.Data;
using SportCity.Web;
using Microsoft.OpenApi.Models;
using Serilog;
using SportCity.Infrastructure.Identity;
using SportCity.Web.Filters;
using SportCity.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddAutoMapper(typeof(ApiMappingProfile));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddIdentity<EfApplicationUser, IdentityRole>()
  .AddEntityFrameworkStores<AppIdentityDbContext>()
  .AddDefaultTokenProviders();

string connectionString = builder.Configuration.GetConnectionString("SqliteConnection");  //Configuration.GetConnectionString("DefaultConnection");
string identityConnectionString = builder.Configuration.GetConnectionString("SqliteIdentityConnection");

builder.Services.AddDbContext<AppDbContext>(connectionString);
builder.Services.AddDbContext<AppIdentityDbContext>(identityConnectionString);

builder.Services.AddControllersWithViews().AddNewtonsoftJson();

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  c.EnableAnnotations();
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: 'Bearer 12345abcdef'",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });
  c.AddSecurityRequirement(new OpenApiSecurityRequirement()
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"},
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,
      },
      new List<string>()
    }
  });
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
}
else
{
  app.UseHsts();
}
app.UseRouting();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

app.UseEndpoints(endpoints =>
{
  endpoints.MapDefaultControllerRoute();
});


using (var scope = app.Services.CreateScope())
{
  var scopedProvider = scope.ServiceProvider;
  try
  {
    var identityContext = scopedProvider.GetRequiredService<AppDbContext>();
    identityContext.Database.EnsureCreated();
  }
  catch (Exception ex)
  {
    app.Logger.LogError(ex, "An error occurred setting the DB.");
  }
}

using (var scope = app.Services.CreateScope())
{
  var scopedProvider = scope.ServiceProvider;
  try
  {
    var userManager = scopedProvider.GetRequiredService<UserManager<EfApplicationUser>>();
    var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var identityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
    identityContext.Database.EnsureCreated();
    await AppIdentityDbContextSeed.SeedAsync(identityContext, userManager, roleManager);
  }
  catch (Exception ex)
  {
    app.Logger.LogError(ex, "An error occurred seeding the DB.");
  }
}

app.Run();
