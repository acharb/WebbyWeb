using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebbyWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebbyWeb.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HabitContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProfileController(HabitContext context, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("UserName,Password")] Profile profile) //binding to Profile class
        {
            bool rememberMe=false;
            if(Request.Form["RememberMe"].ToString()=="on")
                rememberMe = true;

            if (ModelState.IsValid)
            {
                //saves login info as cookie if .RememberMe is true, else false. Last parameter doesn't lock user out if login fail

                var result = await _signInManager.PasswordSignInAsync(profile.UserName, profile.Password, rememberMe, false);

                if(result.Succeeded)
                    return RedirectToAction("Habits", "Home");
                
                ModelState.AddModelError("","Invalid Login Attempt");
                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Index", "Home", profile);

           
            //if(username==profile.UserName)
            //{
            //    //creating session for w/ username and profile Id
            //    HttpContext.Session.SetString("Username",profile.UserName);
            //    HttpContext.Session.SetInt32("ProfileId",profile.ID);


            //    return RedirectToAction("Habits","Home");
            //}
            //else{
            //    return RedirectToAction("ProfileNotFound","Home");
            //}
        }

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profile.ToListAsync());
        }

        [HttpPost]  //post method to DB to save user
        public async Task<IActionResult> Register(RegistrationViewModel profile)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser { UserName = profile.UserName, };
                var result = await _userManager.CreateAsync(user, profile.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false); //signs in if registrationw orks, not persistent, logs out if page left
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    foreach (var error in result.Errors) //adding errors descriptions to model state
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
            }
            return RedirectToAction("Index","Home",profile);
            
        }

        // GET: Profile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .SingleOrDefaultAsync(m => m.ID == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserName,Password")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                var id = profile.ID;
                return RedirectToAction("Habits","Home",id);
            }
            return View(profile);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile.SingleOrDefaultAsync(m => m.ID == id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserName,Password")] Profile profile)
        {
            if (id != profile.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.ID))
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
            return View(profile);
        }

        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .SingleOrDefaultAsync(m => m.ID == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profile.SingleOrDefaultAsync(m => m.ID == id);
            _context.Profile.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return _context.Profile.Any(e => e.ID == id);
        }
    }
}
