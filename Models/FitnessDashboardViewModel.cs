using System.Collections.Generic;

namespace AlphaWorkout.Models
{
    public class FitnessDashboardViewModel
    {
        public List<Steps> Steps { get; set; }
        public List<WaterIntake> WaterIntakes { get; set; }
        public List<CalorieIntake> CalorieIntakes { get; set; }
        public List<RunningDistance> RunningDistances { get; set; }
        public List<Sleep> Sleeps { get; set; }
    }
}
