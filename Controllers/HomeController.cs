using AlphaWorkout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using AlphaWorkout.Data;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AlphaWorkout.Services; // for generating wp with chatgpt

namespace AlphaWorkout.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ChatGPTService _chatGPTService; // for generating wp with chatgpt

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ChatGPTService chatGPTService)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _chatGPTService = chatGPTService; // for generating wp with chatgpt
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
            var weightEntries = _context.WeightEntries.Where(w => w.UserId == user.Id).OrderBy(w => w.Date).ToList();

            string weightChange = null;
            if (weightEntries.Count > 1)
            {
                var latestWeight = weightEntries.Last().Weight;
                var previousWeight = weightEntries[^2].Weight;
                weightChange = latestWeight > previousWeight ? "Increase" : latestWeight < previousWeight ? "Decrease" : "No Change";
            }

            var model = new ProfilePageViewModel
            {
                Username = user.UserName,
                FitnessGoals = onboarding?.FitnessGoals,
                Demographics = onboarding?.Demographics,
                FitnessLevel = onboarding?.FitnessLevel,
                ExercisePreferences = onboarding?.ExercisePreferences,
                PastInjury = onboarding?.PastInjury,
                CurrentWeight = weightEntries.LastOrDefault()?.Weight,
                WeightEntries = weightEntries,
                WorkoutPlanText = onboarding?.WorkoutPlan, // display saved workout plan
                ProfilePicturePath = user.ProfilePicturePath
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
            if (onboarding == null)
            {
                ModelState.AddModelError("", "Please complete the onboarding process first.");
                return RedirectToAction("ProfilePage");
            }

            // Check if a workout plan already exists
            if (!string.IsNullOrEmpty(onboarding.WorkoutPlan))
            {
                // If a workout plan already exists, no need to generate a new one
                ModelState.AddModelError("", "A workout plan has already been generated.");
                return RedirectToAction("ProfilePage");
            }

            // Generate the workout plan using the ChatGPTService
            string workoutPlan = await _chatGPTService.GenerateWorkoutPlan(
                onboarding.FitnessGoals,
                onboarding.ExercisePreferences,
                onboarding.PastInjury,
               // onboarding.PreferredSplit, // Pass the preferred split here
                onboarding.FitnessLevel
            );

            // Save the generated workout plan to the database
            onboarding.WorkoutPlan = workoutPlan;
            _context.Onboardings.Update(onboarding);
            await _context.SaveChangesAsync();

            // Pass the generated plan to the view
            var weightEntries = _context.WeightEntries.Where(w => w.UserId == user.Id).OrderBy(w => w.Date).ToList();

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
                WorkoutPlanText = workoutPlan,  // Plan generated by ChatGPT and saved
                ProfilePicturePath = user.ProfilePicturePath
            };

            return View("ProfilePage", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorkoutPlan()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var onboarding = _context.Onboardings.FirstOrDefault(o => o.UserId == user.Id);
            if (onboarding != null)
            {
                onboarding.WorkoutPlan = string.Empty;
                _context.Update(onboarding);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ProfilePage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadProfilePicture(ProfilePictureViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                string uniqueFileName = null;

                if (model.ProfilePicture != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profilepictures");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfilePicture.CopyToAsync(fileStream);
                    }
                }

                user.ProfilePicturePath = "/profilepictures/" + uniqueFileName;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ProfilePage");
                }
            }
            return RedirectToAction("ProfilePage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSteps(int steps, int stepsGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var stepsEntry = new Steps
            {
                UserId = user.Id,
                StepCount = steps,
                DailyGoal = stepsGoal,
                Date = DateTime.Now
            };

            _context.Steps.Add(stepsEntry);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Steps added: {steps}, Goal: {stepsGoal}");

            return RedirectToAction("ProfilePage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWaterIntake(double waterIntake, double waterGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var waterEntry = new WaterIntake
            {
                UserId = user.Id,
                Liters = waterIntake,
                DailyGoal = waterGoal,
                Date = DateTime.Now
            };

            _context.WaterIntakes.Add(waterEntry);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Water intake added: {waterIntake}, Goal: {waterGoal}");

            return RedirectToAction("ProfilePage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCalorieIntake(int calories, int calorieGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var calorieEntry = new CalorieIntake
            {
                UserId = user.Id,
                Calories = calories,
                DailyGoal = calorieGoal,
                Date = DateTime.Now
            };

            _context.CalorieIntakes.Add(calorieEntry);
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRunningDistance(double distance, double distanceGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var runningEntry = new RunningDistance
            {
                UserId = user.Id,
                DistanceKm = distance,
                DailyGoal = distanceGoal,
                Date = DateTime.Now
            };

            _context.RunningDistances.Add(runningEntry);
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSleep(double sleep, double sleepGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var sleepEntry = new Sleep
            {
                UserId = user.Id,
                Hours = sleep,
                DailyGoal = sleepGoal,
                Date = DateTime.Now
            };

            _context.Sleeps.Add(sleepEntry);
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetTargetWeight(double targetWeight)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userGoal = _context.UserGoals.FirstOrDefault(g => g.UserId == user.Id);
            if (userGoal == null)
            {
                userGoal = new UserGoal { UserId = user.Id };
                _context.UserGoals.Add(userGoal);
            }
            userGoal.TargetWeight = targetWeight;
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }

        // Action method to set the water intake goal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetWaterGoal(double waterGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userGoal = _context.UserGoals.FirstOrDefault(g => g.UserId == user.Id);
            if (userGoal == null)
            {
                userGoal = new UserGoal { UserId = user.Id };
                _context.UserGoals.Add(userGoal);
            }
            userGoal.TargetWaterIntake = waterGoal;
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }

        // Action method to set the sleep goal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetSleepGoal(double sleepGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userGoal = _context.UserGoals.FirstOrDefault(g => g.UserId == user.Id);
            if (userGoal == null)
            {
                userGoal = new UserGoal { UserId = user.Id };
                _context.UserGoals.Add(userGoal);
            }
            userGoal.TargetSleep = sleepGoal;
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }

        // Action method to set the steps goal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetStepsGoal(int stepsGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userGoal = _context.UserGoals.FirstOrDefault(g => g.UserId == user.Id);
            if (userGoal == null)
            {
                userGoal = new UserGoal { UserId = user.Id };
                _context.UserGoals.Add(userGoal);
            }
            userGoal.TargetSteps = stepsGoal;
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }

        // Action method to set the running distance goal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetRunningGoal(double distanceGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userGoal = _context.UserGoals.FirstOrDefault(g => g.UserId == user.Id);
            if (userGoal == null)
            {
                userGoal = new UserGoal { UserId = user.Id };
                _context.UserGoals.Add(userGoal);
            }
            userGoal.TargetRunningDistance = distanceGoal;
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }

        // Action method to set the calorie goal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetCalorieGoal(int calorieGoal)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userGoal = _context.UserGoals.FirstOrDefault(g => g.UserId == user.Id);
            if (userGoal == null)
            {
                userGoal = new UserGoal { UserId = user.Id };
                _context.UserGoals.Add(userGoal);
            }
            userGoal.TargetCalorieIntake = calorieGoal;
            await _context.SaveChangesAsync();

            return RedirectToAction("ProfilePage");
        }


        public async Task<IActionResult> FitnessDashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userGoal = _context.UserGoals.FirstOrDefault(g => g.UserId == user.Id);

            var model = new FitnessDashboardViewModel
            {
                ProfilePicturePath = user.ProfilePicturePath,
                Username = user.UserName,

                // get and set target goals
                TargetWeight = userGoal?.TargetWeight,
                TargetWaterIntake = userGoal?.TargetWaterIntake,
                TargetSteps = userGoal?.TargetSteps,
                TargetCalorieIntake = userGoal?.TargetCalorieIntake,
                TargetRunningDistance = userGoal?.TargetRunningDistance,
                TargetSleep = userGoal?.TargetSleep,

                // tracking entries
                Steps = _context.Steps.Where(s => s.UserId == user.Id).OrderBy(s => s.Date).ToList(),
                WaterIntakes = _context.WaterIntakes.Where(w => w.UserId == user.Id).OrderBy(w => w.Date).ToList(),
                CalorieIntakes = _context.CalorieIntakes.Where(c => c.UserId == user.Id).OrderBy(c => c.Date).ToList(),
                RunningDistances = _context.RunningDistances.Where(r => r.UserId == user.Id).OrderBy(r => r.Date).ToList(),
                Sleeps = _context.Sleeps.Where(s => s.UserId == user.Id).OrderBy(s => s.Date).ToList(),
                WeightEntries = _context.WeightEntries.Where(w => w.UserId == user.Id).OrderBy(w => w.Date).ToList()
            };

            return View(model);
        }

        // feedback form 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitFeedback(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback
                {
                    UserId = _userManager.GetUserId(User),
                    Message = model.Message,
                    SubmittedOn = DateTime.Now
                };

                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();

                return RedirectToAction("ProfilePage");
            }

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
