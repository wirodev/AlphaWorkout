namespace AlphaWorkout.Models
{
    public class Feedback
    {
        public int Id { get; set; }  // pk
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedOn { get; set; }
    }
}
