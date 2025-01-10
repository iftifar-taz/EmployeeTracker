using EmployeeManager.Infrastructure.Persistence;
using EmployeeManager.Web.Configurations;
using EmployeeManager.Web.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLoging();

builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureSwagger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.ApplyPendingMigrations();

app.UseRequestLogging();
app.UseSwaggerIfDevelopment();

app.UseHttpsRedirection();
app.UseCorseForAll();

app.UseMiddleware<RateLimitingMiddleware>();
app.UseJwtAuthentication();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
