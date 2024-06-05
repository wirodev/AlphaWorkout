using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AlphaWorkout.Models
{
    public class ProfilePictureViewModel
    {
        [Required]
        public IFormFile ProfilePicture { get; set; }
    }
}
