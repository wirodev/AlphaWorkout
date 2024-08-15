namespace AlphaWorkout.Models
{
    public class RunningDistance
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public double DistanceKm { get; set; }
        public double DailyGoal { get; set; }
    }
}
