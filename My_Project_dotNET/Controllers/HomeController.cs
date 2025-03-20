using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using My_Project_dotNET.Models;
using Microsoft.Extensions.Logging;

namespace My_Project_dotNET.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        private IConfiguration configuration;

        public HomeController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        #region Dashboard
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Index()
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");

                // Create DataTables for quizzes and questions

                DataTable quizTable = new DataTable();
                DataTable questionTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    // Get recent quizzes
                    using (SqlCommand quizCommand = new SqlCommand("PR_Quiz_Get_Recent_Quizzes", connection))
                    {
                        quizCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader quizReader = quizCommand.ExecuteReader())
                        {
                            quizTable.Load(quizReader);
                        }
                    }

                    // Get recent questions
                    using (SqlCommand questionCommand = new SqlCommand("PR_Question_Get_Recent_Questions", connection))
                    {
                        questionCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader questionReader = questionCommand.ExecuteReader())
                        {
                            questionTable.Load(questionReader);
                        }
                    }
                }

                // Pass both tables to ViewData

                ViewData["RecentQuizzes"] = quizTable;
                ViewData["RecentQuestions"] = questionTable;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the dashboard: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        #region Privacy & Error
        public IActionResult Privacy()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the Privacy page: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Error()
        {
            try
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the Error page: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

    }
}
