using System.Collections.Generic;

namespace AlphaWorkout.Models
{
    public class ProfilePageViewModel
    {
        public string Username { get; set; }
        public string FitnessGoals { get; set; }
        public string Demographics { get; set; }
        public string FitnessLevel { get; set; }
        public string ExercisePreferences { get; set; }
        public string PastInjury { get; set; }

        // properties for weight tracking
        public double? CurrentWeight { get; set; }
        public List<WeightEntry> WeightEntries { get; set; }
        public string WeightChange { get; set; }

        // property for workout plan
        public Dictionary<string, string> WorkoutPlan { get; set; }
    }
}
