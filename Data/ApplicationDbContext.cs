using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AlphaWorkout.Models;

namespace AlphaWorkout.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Onboarding> Onboardings { get; set; }
        public DbSet<WeightEntry> WeightEntries { get; set; }
        public DbSet<Steps> Steps { get; set; }
        public DbSet<WaterIntake> WaterIntakes { get; set; }
        public DbSet<CalorieIntake> CalorieIntakes { get; set; }
        public DbSet<RunningDistance> RunningDistances { get; set; }
        public DbSet<Sleep> Sleeps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
