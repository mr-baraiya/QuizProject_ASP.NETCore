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

        #region QuizWiseQuestionList
        public IActionResult QuizWiseQuestionList()
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PR_MST_Quiz_SelectAll";
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);
                            QuizDropDown();
                            return View(table);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching quiz-wise questions: " + ex.Message;
                return RedirectToAction("QuizWiseQuestionList");
            }
        }
        #endregion

        #region QuizWiseQuestionDelete
        public IActionResult QuizWiseQuestionDelete(int QuizWiseQuestionsID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PR_MST_QuizWiseQuestions_Delete";
                        command.Parameters.Add("@quizWiseQuestionsID", SqlDbType.Int).Value = QuizWiseQuestionsID;
                        command.ExecuteNonQuery();
                    }
                }

                TempData["SuccessMessage"] = "QuizWiseQuestion deleted successfully.";
                return RedirectToAction("QuizWiseQuestionDetails", new { QuizId = 3 });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the QuizWiseQuestion: " + ex.Message;
                return RedirectToAction("QuizWiseQuestionDetails", new { QuizId = 3 });
            }
        }
        #endregion

        #region AddQuizWiseQuestion
        [HttpGet]
        public IActionResult AddQuizWiseQuestion()
        {
            try
            {
                QuestionDropDown();
                QuizDropDown();
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while preparing the Add QuizWiseQuestion page: " + ex.Message;
                return RedirectToAction("QuizWiseQuestionList");
            }
        }
        #endregion

        #region QuizWiseQuestionDetails
        public IActionResult QuizWiseQuestionDetails(int quizId)
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                QuizDetailsViewModel quizDetails = new QuizDetailsViewModel();
                quizDetails.Questions = new List<QuizQuestionViewModel>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PR_MST_QuizWiseQuestions_SelectAll";
                        command.Parameters.AddWithValue("@QuizID", quizId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Assign Quiz Name and Total Questions (only once)
                                    if (string.IsNullOrEmpty(quizDetails.QuizName))
                                    {
                                        quizDetails.QuizName = reader["QuizName"].ToString();
                                        quizDetails.TotalQuestions = Convert.ToInt32(reader["TotalQuestions"]);
                                    }

                                    // Add each question to the list
                                    quizDetails.Questions.Add(new QuizQuestionViewModel
                                    {
                                        QuestionId = Convert.ToInt32(reader["QuestionId"]),
                                        QuestionText = reader["QuestionText"].ToString(),
                                        OptionA = reader["OptionA"].ToString(),
                                        OptionB = reader["OptionB"].ToString(),
                                        OptionC = reader["OptionC"].ToString(),
                                        OptionD = reader["OptionD"].ToString(),
                                        CorrectOption = reader["CorrectOption"].ToString(),
                                        QuestionMarks = Convert.ToInt32(reader["QuestionMarks"])
                                    });
                                }
                            }
                        }
                    }
                }

                return View(quizDetails); // Pass the model to the view
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading QuizWiseQuestion details: " + ex.Message;
                return RedirectToAction("QuizWiseQuestionList");
            }
        }

        #endregion

        #region QuizWiseQuestionAddEdit
        [HttpGet]
        public IActionResult QuizWiseQuestionAddEdit(QuizWiseQuestionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = this.configuration.GetConnectionString("ConnectionString");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = connection.CreateCommand())
                        {
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
                            command.Parameters.Add("@UserID", SqlDbType.Int).Value = My_Project_dotNET.model.UserID();
                            command.Parameters.Add("@Modified", SqlDbType.DateTime).Value = model.Modified;

                            command.ExecuteNonQuery();
                        }
                    }

                    QuestionDropDown();
                    QuizDropDown();
                    return RedirectToAction("QuizWiseQuestionList");
                }

                return View("QuizWiseQuestionList", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while adding or updating the QuizWiseQuestion: " + ex.Message;
                return RedirectToAction("QuizWiseQuestionList");
            }
        }
        #endregion

        #region QuizDropDown
        public void QuizDropDown()
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command2 = connection.CreateCommand())
                    {
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.CommandText = "PR_MST_Quiz_Fill_Dropdown";

                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            DataTable dataTable2 = new DataTable();
                            dataTable2.Load(reader2);
                            List<QuizDropDownModel> QuizList = new List<QuizDropDownModel>();

                            foreach (DataRow data in dataTable2.Rows)
                            {
                                QuizDropDownModel model = new QuizDropDownModel
                                {
                                    QuizID = Convert.ToInt32(data["QuizID"]),
                                    QuizName = data["QuizName"].ToString()
                                };

                                QuizList.Add(model);
                            }
                            ViewBag.QuizList = QuizList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching Quiz dropdown data: " + ex.Message;
            }
        }
        #endregion

        #region QuestionDropDown
        public void QuestionDropDown()
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command2 = connection.CreateCommand())
                    {
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.CommandText = "PR_MST_Question_Fill_Dropdown";

                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            DataTable dataTable2 = new DataTable();
                            dataTable2.Load(reader2);
                            List<QuestionDropDownModel> QuestionList = new List<QuestionDropDownModel>();

                            foreach (DataRow data in dataTable2.Rows)
                            {
                                QuestionDropDownModel model = new QuestionDropDownModel
                                {
                                    QuestionID = Convert.ToInt32(data["QuestionID"]),
                                    QuestionText = data["QuestionText"].ToString()
                                };

                                QuestionList.Add(model);
                            }
                            ViewBag.QuestionList = QuestionList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching Question dropdown data: " + ex.Message;
            }
        }
        #endregion

    }
}
