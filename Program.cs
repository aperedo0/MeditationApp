using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MeditationApp.Data;
using MeditationApp.Models;
using Microsoft.OpenApi.Models;
using AutoMapper;
using MeditationApp.Dtos;
using DotNetEnv;
using Microsoft.AspNetCore.DataProtection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// ✅ Load Environment Variables
Env.Load();
var connectionString = Env.GetString("DB_CONNECTION_STRING") ?? "";

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Error: DB_CONNECTION_STRING is missing in .env file.");
}

// ✅ Data Protection (Consider removing persistence if testing)
builder.Services.AddDataProtection()
    .SetApplicationName("MeditationApp");

// ✅ Database Connection (Fix: Actually uses the .env connection string)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ✅ Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ✅ Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Meditation API",
        Version = "v1",
        Description = "An API for managing meditation sessions"
    });
});

// ✅ Configure WebHost for External Access
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

var app = builder.Build();

// ✅ Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meditation API v1");
    c.RoutePrefix = string.Empty; // Makes Swagger load at `/`
});

// ✅ Middleware Setup
app.UseHttpsRedirection();
app.UseRouting(); // ✅ Fix: Ensures routing works
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ✅ Enable Developer Exception Page in Development Mode
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    
    // ✅ Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meditation API v1");
        c.RoutePrefix = string.Empty; // Makes Swagger load at `/`
    });
}

app.Run();
