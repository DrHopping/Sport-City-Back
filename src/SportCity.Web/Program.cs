using Autofac.Extensions.DependencyInjection;
using Serilog;
using SportCity.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
builder.AddAutoMapper();
builder.AddIdentity();
builder.AddAuthentication();
builder.AddAppSettings();
builder.AddAppDb();
builder.AddSwagger();
builder.AddContainers();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseApplicationMiddleware();

app.MapControllers();

await app.SeedAppDb();
await app.SeedIdentityDb();

app.Run();
