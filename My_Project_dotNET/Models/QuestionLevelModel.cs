using System.ComponentModel.DataAnnotations;


namespace My_Project_dotNET.Models
{
    public class QuestionLevelModel
    {
        [Key]
        public int QuestionLevelID { get; set; }

        [Required(ErrorMessage = "Please enter a question level.")]
        [StringLength(100, ErrorMessage = "Question level can't be longer than 100 characters.")]
        public string QuestionLevel { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a valid positive number.")]
        public int UserID { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow; // Auto-assign timestamp

        public DateTime Modified { get; set; } = DateTime.UtcNow; // Auto-update on modification
    }

    public class QuestionLevelDropDownModel
    {
        public int QuestionLevelID { get; set; }
        public string QuestionLevel { get; set; }
    }
}