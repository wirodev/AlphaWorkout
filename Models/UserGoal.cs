namespace AlphaWorkout.Models
{
    public class UserGoal
    {
        public int Id { get; set; }
        public string UserId { get; set; } // fk
        public double? TargetWeight { get; set; }
        public double? TargetWaterIntake { get; set; }
        public int? TargetSteps { get; set; }
        public double? TargetCalorieIntake { get; set; }
        public double? TargetRunningDistance { get; set; }
        public double? TargetSleep { get; set; }
    }
}
