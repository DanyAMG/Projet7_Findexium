using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class RegisterModel
    {
        // TODO: implement properties needeed for login model.
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
