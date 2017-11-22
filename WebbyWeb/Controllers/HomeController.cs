using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebbyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace WebbyWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly HabitContext _context;

        public HomeController(HabitContext context)
        {
            _context = context;
        }
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
            Console.WriteLine(Habit.Name);
            Console.WriteLine(Habit.Time);
            Console.WriteLine(Habit.Description);

            //DbContextOptions<HabitContext> options = new DbContextOptions<Data.HabitContext>();
            //context = new HabitContext(options);

            //context.Add(Habit);

            return RedirectToAction("StartHabits");
        }
        [Authorize]
        public async Task<IActionResult> Habits(int id)
        {
            
            var ret = await _context.Habit.Where(x=> x.ProfileId==id) .ToListAsync();
            return View(ret);
            
            
        }


        public IActionResult Progress()
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
