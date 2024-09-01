using System.Collections.Generic;

namespace AlphaWorkout.Models
{
    public class ProfilePageViewModel
    {
        public string Username { get; set; }
        public string NewUsername { get; set; }
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

        // property for profile picture
        public string ProfilePicturePath { get; set; }

        // properties to display the user's goals
        public double? TargetWeight { get; set; }
        public double? TargetWaterIntake { get; set; }
        public int? TargetSteps { get; set; }
        public double? TargetCalorieIntake { get; set; }
        public double? TargetRunningDistance { get; set; }
        public double? TargetSleep { get; set; }
    }
}
