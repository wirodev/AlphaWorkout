using System;
using System.Collections.Generic;

namespace AlphaWorkout.Models
{
    public class FitnessDashboardViewModel
    {
        // User Profile Information
        public string ProfilePicturePath { get; set; }
        public string Username { get; set; }

        // Target values set by the user
        public double? TargetWeight { get; set; }
        public double? TargetWaterIntake { get; set; }
        public int? TargetSteps { get; set; }
        public double? TargetCalorieIntake { get; set; }
        public double? TargetRunningDistance { get; set; }
        public double? TargetSleep { get; set; }

        // Lists of tracking entries
        public List<WeightEntry> WeightEntries { get; set; }
        public List<WaterIntake> WaterIntakes { get; set; }
        public List<Steps> Steps { get; set; }
        public List<CalorieIntake> CalorieIntakes { get; set; }
        public List<RunningDistance> RunningDistances { get; set; }
        public List<Sleep> Sleeps { get; set; }
    }
}
