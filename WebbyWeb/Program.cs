using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebbyWeb.Data;
using Microsoft.Extensions.DependencyInjection;
using WebbyWeb.Models;

namespace WebbyWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            using (var context = new HabitContext() ) {

                context.Add(new Habit
                {
                    Name = "Alec",
                    Time = "08:00:00",
                    Description = "Testing"
                });
                context.SaveChanges();
            }

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
