using System.ComponentModel.DataAnnotations;


namespace My_Project_dotNET.Models
{
    public class QuizWiseQuestionsModel
    {
        [Key]
        public int QuizWiseQuestionsID { get; set; }

        [Required(ErrorMessage = "Please select a quiz.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quiz ID must be a valid positive number.")]
        public int QuizID { get; set; }

        [Required(ErrorMessage = "Please select a question.")]
        [Range(1, int.MaxValue, ErrorMessage = "Question ID must be a valid positive number.")]
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "Please select a user.")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a valid positive number.")]
        public int UserID { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow; // Auto-assign current date-time

        public DateTime Modified { get; set; } = DateTime.UtcNow; // Auto-update on insert/update
    }
}