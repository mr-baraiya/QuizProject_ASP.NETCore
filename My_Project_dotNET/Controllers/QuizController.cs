using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace My_Project_dotNET.Controllers
{
    public class QuizController : Controller
    {
        private IConfiguration configuration;

        public QuizController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public IActionResult QuizList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Quiz_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult AddQuiz()
        {
            return View();
        }

        public IActionResult QuizDelete(int QuizID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_Quiz_Delete";
                    command.Parameters.Add("@QuizID", SqlDbType.Int).Value = QuizID;


                    command.ExecuteNonQuery();
                }

                TempData["Message"] = "Quiz deleted successfully.";
                TempData["Type"] = "Success";
                return RedirectToAction("QuizList");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while deleting the Quiz: " + ex.Message;
                TempData["Type"] = "Success";
                return RedirectToAction("QuizList");
            }
        }
    }
}
