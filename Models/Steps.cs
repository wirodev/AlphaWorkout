namespace AlphaWorkout.Models
{
    public class Steps
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int StepCount { get; set; }
        public int DailyGoal { get; set; }
    }
}
