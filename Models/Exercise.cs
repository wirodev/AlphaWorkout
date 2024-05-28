using System.ComponentModel.DataAnnotations;

namespace AlphaWorkout.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Muscle { get; set; }

        [Required]
        public string Difficulty { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}
