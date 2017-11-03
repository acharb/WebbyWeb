using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace WebbyWeb.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StartHabits()
        {

            return View();
        }

        public IActionResult SaveHabit(Models.Habit Habit)
        {
            //Console.WriteLine(Habit.Name);
            //Console.WriteLine(Habit.Time);
            //Console.WriteLine(Habit.Description);

            //DbContextOptions<HabitContext> options = new DbContextOptions<Data.HabitContext>();
            //context = new HabitContext(options);

            //context.Add(Habit);

            return RedirectToAction("StartHabits");
        }

        public IActionResult Habits()
        {


            return View();
        }

        public IActionResult DailyProgress()
        {
            

            return View();
        }

        public IActionResult OverallProgress()
        {


            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
