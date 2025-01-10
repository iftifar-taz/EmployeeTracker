using Identity.Core.Domain.Entities;
using Identity.Infrastructure.Persistence;
using Identity.Web.Configurations;
using Identity.Web.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLoging();

builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureSwagger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

await app.ApplyPendingMigrations();

app.UseRequestLogging();
app.UseSwaggerIfDevelopment();

app.UseHttpsRedirection();
app.UseCorseForAll();

app.UseMiddleware<RateLimitingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
