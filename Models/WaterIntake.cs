namespace AlphaWorkout.Models
{
    public class WaterIntake
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public double Liters { get; set; }
        public double DailyGoal { get; set; }
    }
}
