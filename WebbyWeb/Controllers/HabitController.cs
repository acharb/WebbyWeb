using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebbyWeb.Models;

namespace WebbyWeb.Controllers
{
    public class HabitController : Controller
    {
        private readonly HabitContext _context;

        public HabitController(HabitContext context)
        {
            _context = context;
        }

        // GET: Habit
        public async Task<IActionResult> Index()
        {
            return View(await _context.Habit.ToListAsync());
        }

        // GET: Habit/Details/5, changed
        public WebbyWeb.Models.Habit Details(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var habit = _context.Habit
                .SingleOrDefault(m => m.ID == id);
            if (habit == null)
            {
                return null;
            }

            return habit;
        }

        //GET detail Data, changed
        public async Task<IEnumerable<WebbyWeb.Models.Habit>> DetailData()
        {
            var habit = await _context.Habit.ToListAsync();
            return habit;
        }

        public async Task<IEnumerable<WebbyWeb.Models.Habit>> GetHabitsForProfile()
        {
            try
            {
                var profileName = User.Identity.Name;
                var habits = await _context.Habit.Where(x=> x.ProfileName == profileName).ToListAsync();
                return habits;
            }
            catch(Exception exc)
            {
                throw exc;
            }
            
        }

        //GET: Habit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Time,Description,DoneOrNot,ProfileName")] Habit habit)
        {
            try
            {   
                if (ModelState.IsValid)
                {
                    _context.Add(habit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(habit);
            }
            catch(Exception exc)
            {
                throw exc;
            }
        }

        // GET: Habit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habit = await _context.Habit.SingleOrDefaultAsync(m => m.ID == id);
            if (habit == null)
            {
                return NotFound();
            }
            return View(habit);
        }

        // POST: Habit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Time,Description,DoneOrNot,ProfileName")] Habit habit)
        {
            //need profile ID
            if (id != habit.ID)
            {
                return NotFound();
            };
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitExists(habit.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(habit);
        }

        //Edit Habit in DB
        //[Bind("Description,DoneOrNot,ID,Name,ProfileName,Time")] 
        [HttpPost]
        public void EditHabit(Habit habit)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(habit);
                    _context.SaveChangesAsync();
                }
                catch (System.Exception exc)
                {
                    throw exc;
                }
            }
        }
        

        // GET: Habit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habit = await _context.Habit
                .SingleOrDefaultAsync(m => m.ID == id);
            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }

        // POST: Habit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habit = await _context.Habit.SingleOrDefaultAsync(m => m.ID == id);
            _context.Habit.Remove(habit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitExists(int id)
        {
            return _context.Habit.Any(e => e.ID == id);
        }
        public IActionResult TestPost(Habit habit)
        {
            Console.WriteLine(habit);
            return Json(habit);

        }
    }
}
