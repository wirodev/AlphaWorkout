using System;
using System.ComponentModel.DataAnnotations;

namespace AlphaWorkout.Models
{
    public class WeightEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid weight")]
        public double Weight { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
