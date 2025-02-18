using System.ComponentModel.DataAnnotations;

namespace My_Project_dotNET.Models
{
    public class UserModel
    {
        [Key]
        public int UserUD { get; set; }

        [Required(ErrorMessage = "Please enter user name.")]
        [MaxLength(100, ErrorMessage = "Username can't be longer than 100 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.[a-zA-Z])(?=.\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter mobile number.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string Mobile { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

    }
}