using Microsoft.AspNetCore.Mvc;
using AlphaWorkout.Data;
using AlphaWorkout.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

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
            if (ModelState.IsValid)
            {
                var existingOnboarding = _context.Onboardings.FirstOrDefault(o => o.UserId == onboarding.UserId);
                if (existingOnboarding == null)
                {
                    _context.Add(onboarding);
                }
                else
                {
                    existingOnboarding.FitnessGoals = onboarding.FitnessGoals;
                    existingOnboarding.Demographics = onboarding.Demographics;
                    existingOnboarding.FitnessLevel = onboarding.FitnessLevel;
                    existingOnboarding.ExercisePreferences = onboarding.ExercisePreferences;
                    existingOnboarding.PastInjury = onboarding.PastInjury;
                    _context.Update(existingOnboarding);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View("Onboarding", onboarding); 
        }
    }
}
