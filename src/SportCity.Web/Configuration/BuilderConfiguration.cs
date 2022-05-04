using System.Text;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SportCity.Core;
using SportCity.Infrastructure;
using SportCity.Infrastructure.Data;
using SportCity.Infrastructure.Identity;
using SportCity.Infrastructure.Mapping;
using SportCity.SharedKernel;
using SportCity.Web.Mapping;

namespace SportCity.Web.Configuration;

public static class BuilderConfiguration
{
  public static void AddAppDb(this WebApplicationBuilder builder)
  {
    string connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
    builder.Services.AddDbContext<AppDbContext>(connectionString);
  }

  public static void AddIdentity(this WebApplicationBuilder builder)
  {
    builder.Services.AddIdentity<EfApplicationUser, IdentityRole>(options =>
      {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = false;
      })
      .AddEntityFrameworkStores<AppIdentityDbContext>()
      .AddDefaultTokenProviders();
    string identityConnectionString = builder.Configuration.GetConnectionString("SqliteIdentityConnection");
    builder.Services.AddDbContext<AppIdentityDbContext>(identityConnectionString);

  }

  public static void AddAutoMapper(this WebApplicationBuilder builder)
  {
    builder.Services.AddAutoMapper(typeof(ApiMappingProfile), typeof(InfrastructureMappingProfile));
  }

  public static void AddSwagger(this WebApplicationBuilder builder)
  {
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
  }

  public static void AddContainers(this WebApplicationBuilder builder)
  {
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
      containerBuilder.RegisterModule(new DefaultWebModule());
      containerBuilder.RegisterModule(new DefaultCoreModule());
      containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
    });
  }

  public static void AddAppSettings(this WebApplicationBuilder builder)
  {
    var appSettingsSection = builder.Configuration.GetSection("AppSettings");
    builder.Services.Configure<AppSettings>(appSettingsSection);
  }

  public static void AddAuthentication(this WebApplicationBuilder builder)
  {
    var appSettingsSection = builder.Configuration.GetSection("AppSettings");
    var appSettings = appSettingsSection.Get<AppSettings>();
    var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);
    builder.Services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
  }
}
