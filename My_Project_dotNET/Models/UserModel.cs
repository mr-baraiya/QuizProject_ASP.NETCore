using System.ComponentModel.DataAnnotations;

namespace My_Project_dotNET.Models
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public int Mobile { get; set; }
    }
}
