using System.ComponentModel.DataAnnotations;

namespace My_Project_dotNET.Models
{
    public class QuestionModel
    {

        [Key]
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "Please enter the question text.")]
        [MaxLength(100, ErrorMessage = "Question text can't be longer than 100 characters.")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Please select question level.")]
        public int QuestionLevelID { get; set; }


        [Required(ErrorMessage = "Please enter Option A.")]
        [MaxLength(100, ErrorMessage = "Option A can't be longer than 100 characters.")]
        public string OptionA { get; set; }

        [Required(ErrorMessage = "Please enter Option B.")]
        [MaxLength(100, ErrorMessage = "Option B can't be longer than 100 characters.")]
        public string OptionB { get; set; }

        [Required(ErrorMessage = "Please enter Option C.")]
        [MaxLength(100, ErrorMessage = "Option C can't be longer than 100 characters.")]
        public string OptionC { get; set; }

        [Required(ErrorMessage = "Please enter Option D.")]
        [MaxLength(100, ErrorMessage = "Option D can't be longer than 100 characters.")]
        public string OptionD { get; set; }

        [Required(ErrorMessage = "Please select the correct option.")]
        public string CorrectOption { get; set; }

        [Required(ErrorMessage = "Please enter the question mark.")]
        public int QuestionMark { get; set; }
        public bool IsActive { get; set; }
        public int UserID { get; set; }
    }
}