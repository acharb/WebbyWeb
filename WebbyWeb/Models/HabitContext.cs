using System;
using Microsoft.EntityFrameworkCore;
using WebbyWeb.Models;

namespace WebbyWeb.Models
{
    public class HabitContext : DbContext
    {
        public HabitContext (DbContextOptions<HabitContext> options) : base(options) 
        {
            
        }
        public DbSet<WebbyWeb.Models.Habit> Habit { get; set; }
        public DbSet<WebbyWeb.Models.Profile> Profile { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habit>()
            .HasOne(p => p.Profile)
            .WithMany(b => b.ID);
    }

    }
}
