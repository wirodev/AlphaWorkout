namespace AlphaWorkout.Models
{
    public class CalorieIntake
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int Calories { get; set; }
        public int DailyGoal { get; set; }
    }
}
