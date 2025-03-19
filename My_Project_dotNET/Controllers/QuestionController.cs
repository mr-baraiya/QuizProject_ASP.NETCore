using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using My_Project_dotNET.Models;
using OfficeOpenXml;

namespace My_Project_dotNET.Controllers
{
    [CheckAccess]
    public class QuestionController : Controller
    {
        private IConfiguration configuration;

        public QuestionController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        [HttpGet]
        public IActionResult QuestionList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Question_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        public IActionResult QuestionDelete(int QuestionID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_Question_Delete";
                    command.Parameters.Add("@questionID", SqlDbType.Int).Value = QuestionID;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Question deleted successfully.";
                return RedirectToAction("QuestionList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Question: " + ex.Message;
                return RedirectToAction("QuestionList");
            }
        }

        [HttpGet]
        public IActionResult AddQuestion(int QuestionID)
        {
            if (QuestionID == null)
            {
                var m = new QuestionModel
                {
                    Created = DateTime.Now
                };
                QuestionLevelDropDown();
                return View(m);
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Question_SelectByPK";
            command.Parameters.AddWithValue("@QuestionID", QuestionID);
            //command.Parameters.AddWithValue("@UserID", CommonVariable.UserID());
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            QuestionModel model = new QuestionModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.QuestionID = Convert.ToInt32(dataRow["QuestionID"]);
                model.QuestionText = dataRow["QuestionText"].ToString();
                model.QuestionLevelID = Convert.ToInt32(dataRow["QuestionLevelID"]);
                model.OptionA = dataRow["OptionA"].ToString();
                model.OptionB = dataRow["OptionB"].ToString();
                model.OptionC = dataRow["OptionC"].ToString();
                model.OptionD = dataRow["OptionD"].ToString();
                model.CorrectOption = dataRow["CorrectOption"].ToString();
                model.QuestionMarks = Convert.ToInt32(dataRow["QuestionMarks"]);
                model.IsActive = Convert.ToBoolean(dataRow["IsActive"]);
                model.UserID = Convert.ToInt32(My_Project_dotNET.model.UserID());
                //model.Created = dataRow["Created"] != DBNull.Value ? Convert.ToDateTime(dataRow["Created"]) : DateTime.UtcNow;
                //model.Modified = dataRow["Modified"] != DBNull.Value ? Convert.ToDateTime(dataRow["Modified"]) : DateTime.UtcNow;
            }

            QuestionLevelDropDown();
            return View(model);
        }

        public IActionResult QuestionAddEdit(QuestionModel model)
        {
            // Set UserID explicitly
            model.UserID = Convert.ToInt32(My_Project_dotNET.model.UserID());
            Console.WriteLine($"Set UserID in Model: {model.UserID}");

            // Debugging: Check ModelState before proceeding
            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState is NOT valid!");
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        Console.WriteLine($"Field: {key}, Error: {error.ErrorMessage}");
                    }
                }
                QuestionLevelDropDown();
                return RedirectToAction("QuestionList"); // Return view with errors to fix
            }

            try
            {
                Console.WriteLine($"QuestionID: {model.QuestionID}");

                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (model.QuestionID == 0)
                        {
                            command.CommandText = "PR_MST_Question_Insert";
                        }
                        else
                        {
                            command.CommandText = "PR_MST_Question_Update";
                            command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = model.QuestionID;
                        }

                        command.Parameters.Add("@QuestionText", SqlDbType.VarChar).Value = model.QuestionText;
                        command.Parameters.Add("@OptionA", SqlDbType.VarChar).Value = model.OptionA;
                        command.Parameters.Add("@OptionB", SqlDbType.VarChar).Value = model.OptionB;
                        command.Parameters.Add("@OptionC", SqlDbType.VarChar).Value = model.OptionC;
                        command.Parameters.Add("@OptionD", SqlDbType.VarChar).Value = model.OptionD;
                        command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = model.QuestionLevelID;
                        command.Parameters.Add("@CorrectOption", SqlDbType.VarChar).Value = model.CorrectOption;
                        command.Parameters.Add("@QuestionMarks", SqlDbType.Int).Value = model.QuestionMarks;
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID; // Set UserID

                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("QuestionList");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the question.");
                QuestionLevelDropDown();
                return RedirectToAction("QuestionList");
            }
        }


        [HttpGet]
        public void QuestionLevelDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_QuestionLevel_Fill_Dropdown";
            //command2.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariable.UserID();
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<QuestionLevelDropDownModel> QuestionLevelList = new List<QuestionLevelDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                QuestionLevelDropDownModel model = new QuestionLevelDropDownModel();
                model.QuestionLevelID = Convert.ToInt32(data["QuestionLevelID"]);
                model.QuestionLevel = data["QuestionLevel"].ToString();
                QuestionLevelList.Add(model);
            }
            ViewBag.QuestionLevelList = QuestionLevelList;
        }

        [HttpGet]
        public IActionResult ExportQuestionsToExcel()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_MST_Question_SelectAll";

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        DataTable data = new DataTable();
                        data.Load(sqlDataReader);

                        using (var package = new ExcelPackage())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Questions");

                            // Add headers
                            string[] headers = { "QuestionID", "QuestionText", "OptionA", "OptionB", "OptionC", "OptionD", "CorrectOption", "QuestionMarks", "IsActive", "QuestionLevelID", "QuestionLevel", "UserID", "UserName", "Created", "Modified" };
                            for (int i = 0; i < headers.Length; i++)
                            {
                                worksheet.Cells[1, i + 1].Value = headers[i];
                            }

                            // Add data
                            int row = 2;
                            foreach (DataRow item in data.Rows)
                            {
                                worksheet.Cells[row, 1].Value = item["QuestionID"];
                                worksheet.Cells[row, 2].Value = item["QuestionText"];
                                worksheet.Cells[row, 3].Value = item["OptionA"];
                                worksheet.Cells[row, 4].Value = item["OptionB"];
                                worksheet.Cells[row, 5].Value = item["OptionC"];
                                worksheet.Cells[row, 6].Value = item["OptionD"];
                                worksheet.Cells[row, 7].Value = item["CorrectOption"];
                                worksheet.Cells[row, 8].Value = item["QuestionMarks"];
                                worksheet.Cells[row, 9].Value = Convert.ToBoolean(item["IsActive"]);
                                worksheet.Cells[row, 10].Value = item["QuestionLevelID"];
                                worksheet.Cells[row, 11].Value = item["QuestionLevel"];
                                worksheet.Cells[row, 12].Value = item["UserID"];
                                worksheet.Cells[row, 13].Value = item["UserName"];
                                worksheet.Cells[row, 14].Value = item["Created"] != DBNull.Value ? Convert.ToDateTime(item["Created"]) : (DateTime?)null;
                                worksheet.Cells[row, 15].Value = item["Modified"] != DBNull.Value ? Convert.ToDateTime(item["Modified"]) : (DateTime?)null;
                                row++;
                            }

                            var stream = new MemoryStream();
                            package.SaveAs(stream);
                            stream.Position = 0;

                            string excelName = $"Questions-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                        }
                    }
                }
            }
        }

    }
}
