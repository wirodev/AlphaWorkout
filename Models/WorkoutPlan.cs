namespace AlphaWorkout.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PlanText { get; set; }
        public DateTime CreatedDate { get; set; }

        public ApplicationUser User { get; set; }
    }
}
