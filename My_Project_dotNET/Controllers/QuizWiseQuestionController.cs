using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using My_Project_dotNET.Models;

namespace My_Project_dotNET.Controllers
{
    [CheckAccess]
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
            command.CommandText = "PR_MST_Quiz_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            QuizDropDown();
            return View(table);
        }

        public IActionResult QuizWiseQuestionDelete(int QuizWiseQuestionsID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_QuizWiseQuestion_Delete";
                    command.Parameters.Add("@quizWiseQuestionsID", SqlDbType.Int).Value = QuizWiseQuestionsID;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "QuizWiseQuestion deleted successfully.";
                return RedirectToAction("QuizWiseQuestionList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the QuizWiseQuestion: " + ex.Message;
                return RedirectToAction("QuizWiseQuestionList");
            }
        }

        [HttpGet]
        public IActionResult AddQuizWiseQuestion()
        {
            QuestionDropDown();
            QuizDropDown();
            UserDropDown();
            return View();
        }

        [HttpGet]
        public IActionResult QuizWiseQuestionDetails()
        {
            return View();
        }

        [HttpGet]
        public IActionResult QuizWiseQuestionAddEdit(QuizWiseQuestionModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.QuizWiseQuestionsID == 0)
                {
                    command.CommandText = "PR_MST_QuizWiseQuestions_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_QuizWiseQuestions_Update";
                    command.Parameters.Add("@QuizWiseQuestionsId", SqlDbType.Int).Value = model.QuizWiseQuestionsID;
                }
                command.Parameters.Add("@QuizId", SqlDbType.Int).Value = model.QuizID;
                command.Parameters.Add("@QuestionId", SqlDbType.Int).Value = model.QuestionID;

                command.Parameters.Add("@UserId", SqlDbType.Int).Value = model.UserID;


                command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = model.Modified;
                command.ExecuteNonQuery();

                QuestionDropDown();
                UserDropDown();
                QuizDropDown();
                return RedirectToAction("QuizWiseQuestionList");
            }

            return View("QuizWiseQuestionList", model);
        }
        public void UserDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_User_Fill_Dropdown";
            //command2.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariable.UserID();
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<UserDropDownModel> UserList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                UserDropDownModel model = new UserDropDownModel();
                model.UserID = Convert.ToInt32(data["UserID"]);
                model.UserName = data["UserName"].ToString();
                UserList.Add(model);
            }
            ViewBag.UserList = UserList;
        }

        public void QuizDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_Quiz_Fill_Dropdown";
            //command2.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariable.UserID();
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<QuizDropDownModel> QuizList = new List<QuizDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                QuizDropDownModel model = new QuizDropDownModel();
                model.QuizID = Convert.ToInt32(data["QuizID"]);
                model.QuizName = data["QuizName"].ToString();

                QuizList.Add(model);
            }
            ViewBag.QuizList = QuizList;
        }
        public void QuestionDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_Question_Fill_Dropdown";
            //command2.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariable.UserID();
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<QuestionDropDownModel> QuestionList = new List<QuestionDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                QuestionDropDownModel model = new QuestionDropDownModel();
                model.QuestionID = Convert.ToInt32(data["QuestionID"]);
                model.QuestionText = data["QuestionText"].ToString();

                QuestionList.Add(model);
            }
            ViewBag.QuestionList = QuestionList;
        }
    }
}
