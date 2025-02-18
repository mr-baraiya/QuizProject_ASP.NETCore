using System.ComponentModel.DataAnnotations;

namespace My_Project_dotNET.Models
{
    public class QuizModel
    {
        [Key]
        public int QuizID { get; set; }

        [Required(ErrorMessage = "Please Enter QuizName")]
        [StringLength(100)]
        public string QuizName { get; set; }

        [Required(ErrorMessage = "Please Enter Total Questions")]
        [Range(1, int.MaxValue, ErrorMessage = "Total Questions must be greater than 0")]
        public int TotalQuestions { get; set; }

        [Required(ErrorMessage = "Please Select Quiz Date")]
        public DateTime QuizDate { get; set; }

        public int UserID { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}