using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Model
{
    public class RuleNameDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public string JSon { get; set; }
        public string Template { get; set; }
        public string SqlStr { get; set; }
        public string SqlPart { get; set; }
    }
}
