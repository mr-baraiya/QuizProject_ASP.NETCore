using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using My_Project_dotNET.Models;
using OfficeOpenXml;

namespace My_Project_dotNET.Controllers
{
    [CheckAccess]
    public class QuizController : Controller
    {
        private IConfiguration configuration;

        public QuizController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        #region QuizList
        [HttpGet]
        public IActionResult QuizList()
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
                            return View(table);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching quizzes: " + ex.Message;
                return RedirectToAction("QuizList");
            }
        }
        #endregion

        #region QuizDelete
        public IActionResult QuizDelete(int QuizID)
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
                        command.CommandText = "PR_MST_Quiz_Delete";
                        command.Parameters.Add("@QuizID", SqlDbType.Int).Value = QuizID;
                        command.ExecuteNonQuery();
                    }
                }
                TempData["SuccessMessage"] = "Quiz deleted successfully.";
                return RedirectToAction("QuizList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Quiz: " + ex.Message;
                return RedirectToAction("QuizList");
            }
        }
        #endregion

        #region AddQuiz
        [HttpGet]
        public IActionResult AddQuiz(int? QuizID)
        {
            try
            {
                if (QuizID == null)
                {
                    return View(new QuizModel { Created = DateTime.Now });
                }
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PR_MST_Quiz_SelectByPK";
                        command.Parameters.Add("@QuizID", SqlDbType.Int).Value = QuizID;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);
                            QuizModel model = new QuizModel();
                            foreach (DataRow dataRow in table.Rows)
                            {
                                model.QuizID = Convert.ToInt32(dataRow["QuizID"]);
                                model.QuizName = dataRow["QuizName"].ToString();
                                model.TotalQuestions = Convert.ToInt32(dataRow["TotalQuestions"]);
                                model.QuizDate = Convert.ToDateTime(dataRow["QuizDate"]);
                                model.UserID = Convert.ToInt32(My_Project_dotNET.model.UserID());
                                model.Created = dataRow["Created"] != DBNull.Value ? Convert.ToDateTime(dataRow["Created"]) : DateTime.UtcNow;
                                model.Modified = dataRow["Modified"] != DBNull.Value ? Convert.ToDateTime(dataRow["Modified"]) : DateTime.UtcNow;
                            }
                            return View(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving quiz details: " + ex.Message;
                return RedirectToAction("QuizList");
            }
        }
        #endregion

        #region QuizAddEdit
        [HttpPost]
        public IActionResult QuizAddEdit(QuizModel model)
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
                            if (model.QuizID == 0)
                            {
                                command.CommandText = "PR_MST_Quiz_Insert";
                            }
                            else
                            {
                                command.CommandText = "PR_MST_Quiz_Update";
                                command.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                            }
                            command.Parameters.Add("@QuizName", SqlDbType.VarChar).Value = model.QuizName;
                            command.Parameters.Add("@TotalQuestions", SqlDbType.Int).Value = model.TotalQuestions;
                            command.Parameters.Add("@QuizDate", SqlDbType.DateTime).Value = model.QuizDate;
                            command.Parameters.Add("@UserId", SqlDbType.Int).Value = My_Project_dotNET.model.UserID();
                            command.ExecuteNonQuery();
                        }
                    }
                    return RedirectToAction("QuizList");
                }
                return RedirectToAction("QuizList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while adding or updating the quiz: " + ex.Message;
                return RedirectToAction("QuizList");
            }
        }
        #endregion

        #region ExportQuizzesToExcel
        [HttpGet]
        public IActionResult ExportQuizzesToExcel()
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandText = "PR_MST_Quiz_SelectAll";
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            DataTable data = new DataTable();
                            data.Load(sqlDataReader);
                            using (var package = new ExcelPackage())
                            {
                                var worksheet = package.Workbook.Worksheets.Add("Quizzes");
                                string[] headers = { "QuizID", "QuizName", "TotalQuestions", "QuizDate", "UserID", "UserName", "Created", "Modified" };
                                for (int i = 0; i < headers.Length; i++)
                                {
                                    worksheet.Cells[1, i + 1].Value = headers[i];
                                }
                                int row = 2;
                                foreach (DataRow item in data.Rows)
                                {
                                    worksheet.Cells[row, 1].Value = item["QuizID"];
                                    worksheet.Cells[row, 2].Value = item["QuizName"];
                                    worksheet.Cells[row, 3].Value = item["TotalQuestions"];
                                    worksheet.Cells[row, 4].Value = item["QuizDate"] != DBNull.Value ? Convert.ToDateTime(item["QuizDate"]) : (DateTime?)null;
                                    worksheet.Cells[row, 5].Value = item["UserID"];
                                    worksheet.Cells[row, 6].Value = item["UserName"];
                                    worksheet.Cells[row, 7].Value = item["Created"] != DBNull.Value ? Convert.ToDateTime(item["Created"]) : (DateTime?)null;
                                    worksheet.Cells[row, 8].Value = item["Modified"] != DBNull.Value ? Convert.ToDateTime(item["Modified"]) : (DateTime?)null;
                                    row++;
                                }
                                var stream = new MemoryStream();
                                package.SaveAs(stream);
                                stream.Position = 0;
                                string excelName = $"Quizzes-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while exporting quizzes: " + ex.Message;
                return RedirectToAction("QuizList");
            }
        }
        #endregion
    }
}
