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
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a mobile number.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number.")]
        public string Mobile { get; set; }

        public bool IsActive { get; set; } = true; // Default: Active user

        public bool IsAdmin { get; set; } = false; // Default: Normal user

        public DateTime Created { get; set; } = DateTime.UtcNow; // Auto-set current date-time

        public DateTime Modified { get; set; } = DateTime.UtcNow; // Auto-set on insert/update
    }

    public class UserLoginModel
    {
        [Required(ErrorMessage = "Please enter user name.")]
        [MaxLength(100, ErrorMessage = "Username can't be longer than 100 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }
    }
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Current Password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm your new password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }

}
