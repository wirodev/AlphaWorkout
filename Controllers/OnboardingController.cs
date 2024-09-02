using Microsoft.AspNetCore.Mvc;
using AlphaWorkout.Data;
using AlphaWorkout.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AlphaWorkout.Controllers
{
    public class OnboardingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OnboardingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Onboarding
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var onboarding = _context.Onboardings.FirstOrDefault(o => o.UserId == user.Id);
            if (onboarding == null)
            {
                onboarding = new Onboarding { UserId = user.Id };
            }

            return View("Onboarding", onboarding);
        }

        // POST: Onboarding
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveOnboarding(Onboarding onboarding)
        {
            ModelState.Remove("WorkoutPlan"); // exclude WorkoutPlan from validation

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var existingOnboarding = await _context.Onboardings
                    .SingleOrDefaultAsync(o => o.UserId == user.Id);

                if (existingOnboarding == null)
                {
                    onboarding.UserId = user.Id;
                    _context.Onboardings.Add(onboarding);
                }
                else
                {
                    existingOnboarding.FitnessGoals = onboarding.FitnessGoals;
                    existingOnboarding.Demographics = onboarding.Demographics;
                    existingOnboarding.FitnessLevel = onboarding.FitnessLevel;
                    existingOnboarding.ExercisePreferences = onboarding.ExercisePreferences;
                    existingOnboarding.PastInjury = onboarding.PastInjury;
                    //existingOnboarding.PreferredSplit = onboarding.PreferredSplit;

                    _context.Entry(existingOnboarding).State = EntityState.Modified;
                }

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ProfilePage", "Home");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving onboarding data: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving data.");
                }
            }

            return View("Onboarding", onboarding);
        }




    }
}
