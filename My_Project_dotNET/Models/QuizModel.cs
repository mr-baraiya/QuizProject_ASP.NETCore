using System.ComponentModel.DataAnnotations;

namespace My_Project_dotNET.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class QuizModel
    {
        [Key]
        public int QuizID { get; set; }

        [Required(ErrorMessage = "Please enter quiz name.")]
        [StringLength(100, ErrorMessage = "Quiz name can't exceed 100 characters.")]
        public string QuizName { get; set; }

        [Required(ErrorMessage = "Please enter total questions.")]
        [Range(1, int.MaxValue, ErrorMessage = "Total questions must be greater than 0.")]
        public int TotalQuestions { get; set; }

        [Required(ErrorMessage = "Please select quiz date.")]
        [DataType(DataType.DateTime)]
        public DateTime QuizDate { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow; // Auto-assign current date-time

        public DateTime Modified { get; set; } = DateTime.UtcNow; // Auto-update on insert/update
    }
}