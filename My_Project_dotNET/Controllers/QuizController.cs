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

        [HttpGet]
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

                TempData["SuccessMessage"] = "Quiz deleted successfully.";
                TempData["Type"] = "Success";
                return RedirectToAction("QuizList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Quiz: " + ex.Message;
                TempData["Type"] = "Error";
                return RedirectToAction("QuizList");
            }
        }

        [HttpGet]
        public IActionResult AddQuiz(int? QuizID)
        {
            if (QuizID == null)
            {
                var m = new QuizModel
                {
                    Created = DateTime.Now
                };
                return View(m);
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Quiz_SelectByPK";
            command.Parameters.Add("@QuizID", SqlDbType.Int).Value = QuizID;
            SqlDataReader reader = command.ExecuteReader();
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

        [HttpPost]
        public IActionResult QuizAddEdit(QuizModel model)
        {
            if (ModelState.IsValid)
            {

                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
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
                return RedirectToAction("QuizList");
            }

            return RedirectToAction("QuizList");
        }

        [HttpGet]
        public IActionResult ExportQuizzesToExcel()
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

                            // Add headers
                            worksheet.Cells[1, 1].Value = "QuizID";
                            worksheet.Cells[1, 2].Value = "QuizName";
                            worksheet.Cells[1, 3].Value = "TotalQuestions";
                            worksheet.Cells[1, 4].Value = "QuizDate";
                            worksheet.Cells[1, 5].Value = "UserID";
                            worksheet.Cells[1, 6].Value = "UserName";
                            worksheet.Cells[1, 7].Value = "Created";
                            worksheet.Cells[1, 8].Value = "Modified";

                            // Add data
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

    }
}
