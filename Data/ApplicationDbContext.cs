using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MeditationApp.Models;

namespace MeditationApp.Data  // ✅ This groups database-related classes
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MeditationSession> MeditationSessions { get; set; }
    }
}