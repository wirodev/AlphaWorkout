namespace AlphaWorkout.Models
{
    public class Sleep
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public double Hours { get; set; }
        public double DailyGoal { get; set; }
    }
}
