using AlphaWorkout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using AlphaWorkout.Data;
using System;
using System.Collections.Generic;

namespace AlphaWorkout.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ProfilePage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var onboarding = _context.Onboardings.FirstOrDefault(o => o.UserId == user.Id);
            var weightEntries = _context.WeightEntries.Where(w => w.UserId == user.Id).OrderByDescending(w => w.Date).ToList();

            string weightChange = null;
            if (weightEntries.Count > 1)
            {
                var latestWeight = weightEntries.First().Weight;
                var previousWeight = weightEntries.Skip(1).First().Weight;
                if (latestWeight > previousWeight)
                {
                    weightChange = "Increase";
                }
                else if (latestWeight < previousWeight)
                {
                    weightChange = "Decrease";
                }
                else
                {
                    weightChange = "No Change";
                }
            }

            var model = new ProfilePageViewModel
            {
                Username = user.UserName,
                FitnessGoals = onboarding?.FitnessGoals,
                Demographics = onboarding?.Demographics,
                FitnessLevel = onboarding?.FitnessLevel,
                ExercisePreferences = onboarding?.ExercisePreferences,
                PastInjury = onboarding?.PastInjury,
                CurrentWeight = weightEntries.FirstOrDefault()?.Weight,
                WeightEntries = weightEntries,
                WeightChange = weightChange,
                WorkoutPlan = GenerateWorkoutPlan(onboarding, weightEntries) // New method call
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWeight(double weight)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var weightEntry = new WeightEntry
            {
                UserId = user.Id,
                Weight = weight,
                Date = DateTime.Now
            };

            _context.WeightEntries.Add(weightEntry);
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateWorkoutPlan()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var onboarding = _context.Onboardings.FirstOrDefault(o => o.UserId == user.Id);
            var weightEntries = _context.WeightEntries.Where(w => w.UserId == user.Id).OrderByDescending(w => w.Date).ToList();

            var model = new ProfilePageViewModel
            {
                Username = user.UserName,
                FitnessGoals = onboarding?.FitnessGoals,
                Demographics = onboarding?.Demographics,
                FitnessLevel = onboarding?.FitnessLevel,
                ExercisePreferences = onboarding?.ExercisePreferences,
                PastInjury = onboarding?.PastInjury,
                CurrentWeight = weightEntries.FirstOrDefault()?.Weight,
                WeightEntries = weightEntries,
                WorkoutPlan = GenerateWorkoutPlan(onboarding, weightEntries)
            };

            return View("ProfilePage", model);
        }

        private Dictionary<string, string> GenerateWorkoutPlan(Onboarding onboarding, List<WeightEntry> weightEntries)
        {
            // Sample logic for generating a workout plan
            var workoutPlan = new Dictionary<string, string>();

            if (onboarding == null)
            {
                return workoutPlan;
            }

            // Sample logic: 3-day split workout plan
            var exercises = _context.Exercises.ToList();
            var selectedExercises = exercises.Where(e =>
                (onboarding.FitnessGoals == "Build Muscle" && e.Type == "strength") ||
                (onboarding.FitnessGoals == "Lose Weight" && e.Type == "cardio") ||
                (onboarding.ExercisePreferences == null || !onboarding.ExercisePreferences.Contains(e.Name)) &&
                (onboarding.PastInjury == null || !onboarding.PastInjury.Contains(e.Muscle))
            ).ToList();

            workoutPlan["Day 1"] = string.Join(", ", selectedExercises.Take(3).Select(e => e.Name));
            workoutPlan["Day 2"] = string.Join(", ", selectedExercises.Skip(3).Take(3).Select(e => e.Name));
            workoutPlan["Day 3"] = string.Join(", ", selectedExercises.Skip(6).Take(3).Select(e => e.Name));

            return workoutPlan;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
