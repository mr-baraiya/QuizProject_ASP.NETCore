using System.ComponentModel.DataAnnotations;

namespace My_Project_dotNET.Models
{
    public class QuestionModel
    {
        [Key]
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "Please enter the question text.")]
        [MaxLength(500, ErrorMessage = "Question text cannot exceed 500 characters.")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Please select a question level.")]
        [Range(1, int.MaxValue, ErrorMessage = "Question Level ID must be a valid positive number.")]
        public int QuestionLevelID { get; set; }

        [Required(ErrorMessage = "Please enter Option A.")]
        [MaxLength(200, ErrorMessage = "Option A cannot exceed 200 characters.")]
        public string OptionA { get; set; }

        [Required(ErrorMessage = "Please enter Option B.")]
        [MaxLength(200, ErrorMessage = "Option B cannot exceed 200 characters.")]
        public string OptionB { get; set; }

        [Required(ErrorMessage = "Please enter Option C.")]
        [MaxLength(200, ErrorMessage = "Option C cannot exceed 200 characters.")]
        public string OptionC { get; set; }

        [Required(ErrorMessage = "Please enter Option D.")]
        [MaxLength(200, ErrorMessage = "Option D cannot exceed 200 characters.")]
        public string OptionD { get; set; }

        [Required(ErrorMessage = "Please select the correct option.")]
        [RegularExpression("^(A|B|C|D)$", ErrorMessage = "Correct option must be A, B, C, or D.")]
        public string CorrectOption { get; set; }

        [Required(ErrorMessage = "Please enter the question marks.")]
        [Range(1, 100, ErrorMessage = "Question marks must be between 1 and 100.")]
        public int QuestionMarks { get; set; }

        public bool IsActive { get; set; } = true; // Default: Active

        [Required(ErrorMessage = "User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a valid positive number.")]
        public int UserID { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow; // Auto-assign on creation
        public DateTime Modified { get; set; } = DateTime.UtcNow; // Auto-update on modification
    }

}