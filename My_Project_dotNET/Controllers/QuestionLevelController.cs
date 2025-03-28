﻿using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using My_Project_dotNET.Models;
using OfficeOpenXml;

namespace My_Project_dotNET.Controllers
{
    [CheckAccess]
    public class QuestionLevelController : Controller
    {
        private IConfiguration configuration;

        public QuestionLevelController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        #region QuestionLevelList
        public IActionResult QuestionLevelList()
        {
            try
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_MST_QuestionLevel_SelectAll";
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                return View(table);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching QuestionLevels: " + ex.Message;
                return RedirectToAction("QuestionLevelList");
            }
        }
        #endregion

        #region QuestionLevelDelete
        public IActionResult QuestionLevelDelete(int QuestionLevelID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_QuestionLevel_Delete";
                    command.Parameters.Add("@questionLevelID", SqlDbType.Int).Value = QuestionLevelID;

                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "QuestionLevel deleted successfully.";
                return RedirectToAction("QuestionLevelList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the QuestionLevel: " + ex.Message;
                return RedirectToAction("QuestionLevelList");
            }
        }
        #endregion

        #region AddQuestionLevel
        [HttpGet]
        public IActionResult AddQuestionLevel(int QuestionLevelID)
        {
            try
            {
                if (QuestionLevelID == null)
                {
                    var m = new QuestionLevelModel
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
                command.CommandText = "PR_MST_QuestionLevel_SelectByPK";
                command.Parameters.AddWithValue("@QuestionLevelID", QuestionLevelID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                QuestionLevelModel model = new QuestionLevelModel();

                foreach (DataRow dataRow in table.Rows)
                {
                    model.QuestionLevelID = Convert.ToInt32(dataRow["QuestionLevelID"]);
                    model.QuestionLevel = dataRow["QuestionLevel"].ToString();
                    model.UserID = Convert.ToInt32(My_Project_dotNET.model.UserID());
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching the QuestionLevel: " + ex.Message;
                return RedirectToAction("QuestionLevelList");
            }
        }
        #endregion

        #region QuestionLevelAddEdit
        [HttpPost]
        public IActionResult QuestionLevelAddEdit(QuestionLevelModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string connectionString = this.configuration.GetConnectionString("ConnectionString");
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;

                    if (model.QuestionLevelID <= 0)
                    {
                        command.CommandText = "PR_MST_QuestionLevel_Insert";
                    }
                    else
                    {
                        command.CommandText = "PR_MST_QuestionLevel_Update";
                        command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = model.QuestionLevelID;
                    }
                    command.Parameters.Add("@QuestionLevel", SqlDbType.VarChar).Value = model.QuestionLevel;
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = My_Project_dotNET.model.UserID();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("QuestionLevelList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while saving the QuestionLevel: " + ex.Message;
                return RedirectToAction("QuestionLevelList");
            }
        }
        #endregion

        #region ExportQuestionLevelsToExcel
        [HttpGet]
        public IActionResult ExportQuestionLevelsToExcel()
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
                        sqlCommand.CommandText = "PR_MST_QuestionLevel_SelectAll";

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            DataTable data = new DataTable();
                            data.Load(sqlDataReader);

                            using (var package = new ExcelPackage())
                            {
                                var worksheet = package.Workbook.Worksheets.Add("QuestionLevels");

                                worksheet.Cells[1, 1].Value = "QuestionLevelID";
                                worksheet.Cells[1, 2].Value = "QuestionLevel";
                                worksheet.Cells[1, 3].Value = "UserID";
                                worksheet.Cells[1, 4].Value = "Created";
                                worksheet.Cells[1, 5].Value = "Modified";

                                int row = 2;
                                foreach (DataRow item in data.Rows)
                                {
                                    worksheet.Cells[row, 1].Value = item["QuestionLevelID"];
                                    worksheet.Cells[row, 2].Value = item["QuestionLevel"];
                                    worksheet.Cells[row, 3].Value = item["UserID"];
                                    worksheet.Cells[row, 4].Value = item["Created"] != DBNull.Value ? Convert.ToDateTime(item["Created"]) : (DateTime?)null;
                                    worksheet.Cells[row, 5].Value = item["Modified"] != DBNull.Value ? Convert.ToDateTime(item["Modified"]) : (DateTime?)null;
                                    row++;
                                }

                                var stream = new MemoryStream();
                                package.SaveAs(stream);
                                stream.Position = 0;

                                string excelName = $"QuestionLevels-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while exporting QuestionLevels: " + ex.Message;
                return RedirectToAction("QuestionLevelList");
            }
        }
        #endregion

    }
}
