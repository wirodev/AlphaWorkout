// Models/Onboarding.cs
using System.ComponentModel.DataAnnotations;

namespace AlphaWorkout.Models
{
    public class Onboarding
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } // link onboarding info with the user

        [Required]
        public string FitnessGoals { get; set; }

        [Required]
        public string Demographics { get; set; }

        [Required]
        public string FitnessLevel { get; set; }

        public string ExercisePreferences { get; set; }

        public string PastInjury { get; set; }
        //public string PastInjuryDetails { get; set; }
       // public int PreferredSplit { get; set; }
        public string WorkoutPlan { get; set; }
    }
}
