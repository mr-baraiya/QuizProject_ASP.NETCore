using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace My_Project_dotNET.Controllers
{
    public class QuizWiseQuestionController : Controller
    {

        private IConfiguration configuration;

        public QuizWiseQuestionController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public IActionResult QuizWiseQuestionList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_QuizWiseQuestion_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult QuizWiseQuestionDetails()
        {
            return View();
        }

        public IActionResult AddQuizWiseQuestion()
        {
            return View();
        }
    }
}
