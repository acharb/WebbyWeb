using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbyWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace WebbyWeb.Controllers
{

    //[Authorize(AuthenticationSchemes = AuthSchemes)]
    public class HomeController : Controller
    {
        private const string AuthSchemes =
            CookieAuthenticationDefaults.AuthenticationScheme;
        
        private readonly HabitContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(HabitContext context,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //[AllowAnonymous]
        public IActionResult Index() //goes to habits view if logged in, index view if not
        {
            if(!User.Identity.IsAuthenticated) // if not logged in
            {
                return View();
            }
            return RedirectToAction("Habits","Home");
        }
        public IActionResult Welcome()  //welcome page
        {
            return View();
        }

        public IActionResult StartHabits()
        {
            if(User.Identity.IsAuthenticated)
            {
                var progress = GetProgress();
                ViewBag.DayTracker = progress.DayTracker;
                return View();
            }
            return View("NotLoggedIn");
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
        
        public async Task<IActionResult> Habits()
        {
            string profileName = User.Identity.Name;
            if(User.Identity.IsAuthenticated)
            {
                var ret = await _context.Habit.Where(x=> x.ProfileName==profileName).ToListAsync();
                return View(ret);
                
            }
            return View("NotLoggedIn");
        }

        public IActionResult Progress()
        {
            if(User.Identity.IsAuthenticated)
            {
                return View();
            }
            return View("NotLoggedIn");
        }

        public IActionResult OverallProgress()
        {
            if(User.Identity.IsAuthenticated)
            {
                return View();
            }
            return View("NotLoggedIn");
        }

        public IActionResult Login()
        {
            return View();
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
                    return View("Welcome");
                
                ModelState.AddModelError("","Invalid Login Attempt");
                return View(profile);

            }
            
            return View(profile);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
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
                    int add = AddProgressProfile(profile.UserName);
                    await _signInManager.SignInAsync(user, false); //signs in if registrationw orks, not persistent, logs out if page left
                    return RedirectToAction("Welcome");
                }
                else
                {
                    foreach (var error in result.Errors) //adding errors descriptions to model state
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(profile);
                }
            }
            return View(profile);
            
        }
        public int AddProgressProfile(string username)
        {
            try{
                if(username != null)
                {
                    _context.Progress.Add(new Progress{
                        ProfileName=username,
                        WeeklyProgress=0,
                        MonthlyProgress=0,
                        DayTracker =0
                            });
                    _context.SaveChanges();
                    return 1;
                };
                return 0;
            }
            catch(Exception exc)
            {
                throw exc;
                //return 0;
            }
        }

        public WebbyWeb.Models.Progress GetProgress()
        {
            string profileName = User.Identity.Name;
            try
            {
                if (profileName==null)
                    return null;
                
                var progress = _context.Progress.Where(x=>x.ProfileName==profileName).FirstOrDefault();
                if(progress==null)
                    return null;
                return progress;

            }
            catch(Exception exc)
            {
                throw exc;
            }

        }

        public async Task<IActionResult> AddHabitToProgress()
        {
            WebbyWeb.Models.Progress progress = GetProgress();
            progress.NumOfHabits +=1;

            progress.WeeklyPtsPossible = progress.WeeklyPtsPossible + (7-progress.DayTracker)*1;
            progress.MonthlyPtsPossible = progress.MonthlyPtsPossible + (30-progress.DayTracker)*1;
            try
            {
                _context.Update(progress);
                await _context.SaveChangesAsync();
            }
            catch(Exception exc)
            {
                throw exc;
            }
            return RedirectToAction("StartHabits");
        }


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
    }
}
