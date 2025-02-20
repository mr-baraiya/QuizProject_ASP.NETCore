using System.ComponentModel.DataAnnotations;

namespace My_Project_dotNET.Models
{
    public class UserModel
        {
            [Key]
            public int UserID { get; set; }

            [Required(ErrorMessage = "Please enter user name.")]
            [MaxLength(100, ErrorMessage = "Username can't be longer than 100 characters.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Please enter password.")]
            [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
            [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).{8,}$",ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Please enter email.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Please enter mobile number.")]
            [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Please enter a valid phone number.")]
            public string Mobile { get; set; }

            public bool IsActive { get; set; } = true; // Default: Active user

            public bool IsAdmin { get; set; } = false; // Default: Normal user

            public DateTime Created { get; set; } = DateTime.UtcNow; // Auto-set current date-time

            public DateTime Modified { get; set; } = DateTime.UtcNow; // Auto-set on insert/update
    }
}