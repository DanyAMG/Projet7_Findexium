using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Model
{
    public class RatingDTO
    {
        [Required(ErrorMessage = "Moodys Rating is required.")]
        public string MoodysRating { get; set; }
        public string SandPRating { get; set; }
        [Required(ErrorMessage = "Fitch Rating is required.")]
        public string FitchRating { get; set; }
        [Required(ErrorMessage = "Order Number is required.")]
        public byte? OrderNumber { get; set; }
    }
}
