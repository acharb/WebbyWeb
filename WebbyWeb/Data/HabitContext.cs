using System;
using Microsoft.EntityFrameworkCore;
using WebbyWeb.Models;

namespace WebbyWeb.Data
{
    public class HabitContext : DbContext
    {
        public HabitContext(DbContextOptions<HabitContext> options) : base(options)
        {
        }

        public DbSet<Habit> Habits { get; set; }    //creating database set for entity set (datatable aka Habit)
                                                    //note entity = row


    }
}
