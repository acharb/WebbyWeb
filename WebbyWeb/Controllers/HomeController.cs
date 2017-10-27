using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbyWeb.Models;

namespace WebbyWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StartHabits(Models.Habit Habit)
        {
            Console.WriteLine(Habit.Name);

            return View();
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
