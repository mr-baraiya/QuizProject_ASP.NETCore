using System.ComponentModel.DataAnnotations;


namespace My_Project_dotNET.Models
{
    public class QuestionLevelModel
    {
        [Key]
        public int QuestionLevelID { get; set; }

        [Required(ErrorMessage = "Please enter question level")]
        [StringLength(100, ErrorMessage = "Question level can't be longer than 100 characters.")]
        public string QuestionLevel { get; set; }
        public int UserID { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}