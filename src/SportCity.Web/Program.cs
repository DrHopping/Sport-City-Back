﻿using System.Text.Json.Serialization;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using SportCity.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
builder.AddAutoMapper();
builder.AddIdentity();
builder.AddAppSettings();
builder.AddAppDb();
builder.AddSwagger();
builder.AddContainers();
builder.AddAuthentication();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();


var app = builder.Build();

app.UseRouting();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();


// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));


app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseApplicationMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

await app.SeedAppDb();
await app.SeedIdentityDb();

app.Run();
