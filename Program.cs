using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using MeditationApp.Data;
using MeditationApp.Models;
using Microsoft.OpenApi.Models;
using AutoMapper;
using MeditationApp.Dtos;
using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = Env.GetString("DB_CONNECTION_STRING");

// ✅ Add database connection (Using appsettings.json directly)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ✅ Add AutoMapper (Fix: Removed duplicate registration)
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

var app = builder.Build();

// ✅ Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meditation API v1");
    c.RoutePrefix = string.Empty; // Makes Swagger load at `/`
});

// ✅ Ensure proper security middleware
app.UseHttpsRedirection();
app.UseAuthentication();  // 🔥 Added: Authentication middleware
app.UseAuthorization();
app.MapControllers();

// ✅ Enable Developer Exception Page in Development Mode
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Run();
