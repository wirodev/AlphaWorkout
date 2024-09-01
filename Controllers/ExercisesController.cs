﻿using Microsoft.AspNetCore.Mvc;
using AlphaWorkout.Data;
using AlphaWorkout.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AlphaWorkout.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exercises
        public async Task<IActionResult> Index(string searchString)
        {
            // ViewData to preserve the search string
            ViewData["CurrentFilter"] = searchString;

            var exercises = from e in _context.Exercises
                            select e;

            if (!string.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(e => e.Name.Contains(searchString) ||
                                                 e.Type.Contains(searchString) ||
                                                 e.Muscle.Contains(searchString) ||
                                                 e.Difficulty.Contains(searchString) ||
                                                 e.Instructions.Contains(searchString));
            }

            return View(await exercises.ToListAsync());
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Muscle,Difficulty,Instructions")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercise);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Edit/5
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
                    _context.Update(exercise);

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

                return RedirectToAction(nameof(Index));
            }

            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
