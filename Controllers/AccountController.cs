using AlphaWorkout.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AlphaWorkout.Controllers
{
    // handle account-related actions such as registration and login
    public class AccountController : Controller
    {
        // hold the UserManager and SignInManager instances
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // initialize UserManager and SignInManager
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // HTTP GET 
        [HttpGet]
        public IActionResult Register()
        {
            return View(); 
        }

        // HTTP POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // cechk if model state is valid
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email, // username is set to email address
                    Email = model.Email,    
                    FirstName = model.FirstName, 
                    LastName = model.LastName   
                };

                // create the user in the database with their password
                var result = await _userManager.CreateAsync(user, model.Password);

                // check if the user creation was successful
                if (result.Succeeded)
                {
                    // sign in the user if registration was successful
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // redirect to the home/index page after successful registration and sign-in
                    return RedirectToAction("Index", "Home");
                }

                // if user creation failed then add errors to ModelState and display them in the view
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}
