using System.ComponentModel.DataAnnotations;


namespace My_Project_dotNET.Models
{
    public class QuizWiseQuestionsModel
    {
        [Key]
        public int QuizWiseQuestionsID { get; set; }

        [Required(ErrorMessage = "Please select quiz")]
        public int QuizID { get; set; }

        [Required(ErrorMessage = "Please select question")]
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "Please select user name")]
        public int UserID { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }


    }
}