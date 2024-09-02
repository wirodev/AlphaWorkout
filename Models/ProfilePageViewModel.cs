﻿using System.Collections.Generic;

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
        public int PreferredSplit { get; set; }

        // Properties for weight tracking
        public double? CurrentWeight { get; set; }
        public List<WeightEntry> WeightEntries { get; set; }
        public string WeightChange { get; set; }

        // Property for the workout plan generated by ChatGPT
        public string WorkoutPlanText { get; set; }

        // Property for profile picture
        public string ProfilePicturePath { get; set; }

        // Properties to display the user's goals
        public double? TargetWeight { get; set; }
        public double? TargetWaterIntake { get; set; }
        public int? TargetSteps { get; set; }
        public double? TargetCalorieIntake { get; set; }
        public double? TargetRunningDistance { get; set; }
        public double? TargetSleep { get; set; }
    }

}
