using Microsoft.AspNetCore.Mvc;
using AlphaWorkout.Data;
using AlphaWorkout.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AlphaWorkout.Controllers
{
    // Managing exercise entities
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Initialize the database context
        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exercises
        // Retrieve and display a list of all exercises
        public async Task<IActionResult> Index()
        {
            // Fetch all exercises from the database asynchronously
            return View(await _context.Exercises.ToListAsync());
        }

        // GET: Exercises/Create
        // Display a form for creating a new exercise
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // Handle the form submission for creating a new exercise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Muscle,Difficulty,Instructions")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                // Add new exercise to the database context
                _context.Add(exercise);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, return the view with the current exercise model
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        // Display a form for editing an existing exercise
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the exercise by id asynchronously
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            // Return the view with the exercise model
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // Handle the form submission for editing an existing exercise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Muscle,Difficulty,Instructions")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the exercise in the database context
                    _context.Update(exercise);

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, return the view with the current exercise model
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        // Display a confirmation form for deleting an exercise
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the exercise by id asynchronously
            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            // Return the view with the exercise model
            return View(exercise);
        }

        // POST: Exercises/Delete/5
        // Handle the form submission for deleting an exercise
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the exercise by id asynchronously
            var exercise = await _context.Exercises.FindAsync(id);

            // Remove the exercise from the database context
            _context.Exercises.Remove(exercise);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect to the Index action
            return RedirectToAction(nameof(Index));
        }

        // Check if an exercise exists by id
        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
