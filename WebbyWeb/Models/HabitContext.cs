using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebbyWeb.Models;

namespace WebbyWeb.Models
{
    public class HabitContext : IdentityDbContext<ApplicationUser> //Uses identity db context for identity authentication use
    {
        public HabitContext (DbContextOptions<HabitContext> options) : base(options) 
        {
            
        }
        public DbSet<WebbyWeb.Models.Habit> Habit { get; set; }
        public DbSet<WebbyWeb.Models.Profile> Profile { get; set; }

   

    }
}
