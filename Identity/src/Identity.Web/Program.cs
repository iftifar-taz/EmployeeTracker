using System.Text;
using Asp.Versioning;
using Identity.Application.Behaviors.Validators;
using Identity.Application.Features.Sessions.CreateSession;
using Identity.Core.Domain.Entities;
using Identity.Core.Interfaces;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;
using Identity.Web.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Identity.Web.Configurations;

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

await DbSeeder.SeedUsersAndRolesAsync(app.Services);

app.UseRequestLogging();
app.UseSwaggerIfDevelopment(app.Environment);

app.UseHttpsRedirection();
app.UseCorseForAll();

app.UseMiddleware<RateLimitingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
