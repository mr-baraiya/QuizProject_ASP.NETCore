using System.ComponentModel.DataAnnotations;


namespace My_Project_dotNET.Models
{
    public class QuizWiseQuestionModel
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
    public class QuizDetailsViewModel
    {
        public string QuizName { get; set; }
        public int TotalQuestions { get; set; }
        public List<QuizQuestionViewModel> Questions { get; set; }
    }

    public class QuizQuestionViewModel
    {
        public int QuestionId { get; set; }
        public int QuizWiseQuestionId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }
        public int QuestionMarks { get; set; }
    }

}