using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using My_Project_dotNET.Models;
using OfficeOpenXml;

namespace My_Project_dotNET.Controllers
{
    public class UserController : Controller
    {
        private IConfiguration configuration;

        public UserController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        [HttpGet]
        [Route("ShowUsers")]
        public IActionResult UserList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_User_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }

        [HttpGet]
        public IActionResult UserDelete(int UserID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_User_Delete";
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = UserID;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "User deleted successfully.";
                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the User: " + ex.Message;
                return RedirectToAction("UserList");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_MST_User_Insert", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", model.UserName);
                        command.Parameters.AddWithValue("@Password", model.Password);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@Mobile", model.Mobile);
                        command.Parameters.AddWithValue("@IsAdmin", model.IsAdmin);

                        command.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_MST_User_SelectByUserNamePassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@username", model.UserName);
                        command.Parameters.AddWithValue("@Password", model.Password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                HttpContext.Session.SetString("UserName", reader["UserName"].ToString());
                                bool isAdmin = Convert.ToBoolean(reader["IsAdmin"]);

                                return isAdmin ? RedirectToAction("Index", "Home") : RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Invalid credentials.";
                                return View(model);
                            }
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ExportToExcel()
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_MST_User_SelectAll"; // Assuming you have a stored procedure for user data

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        DataTable data = new DataTable();
                        data.Load(sqlDataReader);

                        using (var package = new ExcelPackage())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Users");

                            // Add headers
                            worksheet.Cells[1, 1].Value = "UserID";
                            worksheet.Cells[1, 2].Value = "UserName";
                            worksheet.Cells[1, 3].Value = "Email";
                            worksheet.Cells[1, 4].Value = "Mobile";
                            worksheet.Cells[1, 5].Value = "IsActive";
                            worksheet.Cells[1, 6].Value = "IsAdmin";
                            worksheet.Cells[1, 7].Value = "Created";
                            worksheet.Cells[1, 8].Value = "Modified";

                            // Add data
                            int row = 2;
                            foreach (DataRow item in data.Rows)
                            {
                                worksheet.Cells[row, 1].Value = item["UserID"];
                                worksheet.Cells[row, 2].Value = item["UserName"];
                                worksheet.Cells[row, 3].Value = item["Email"];
                                worksheet.Cells[row, 4].Value = item["Mobile"];
                                worksheet.Cells[row, 5].Value = Convert.ToBoolean(item["IsActive"]);
                                worksheet.Cells[row, 6].Value = Convert.ToBoolean(item["IsAdmin"]);
                                worksheet.Cells[row, 7].Value = Convert.ToDateTime(item["Created"]);
                                worksheet.Cells[row, 8].Value = Convert.ToDateTime(item["Modified"]);
                                row++;
                            }

                            var stream = new MemoryStream();
                            package.SaveAs(stream);
                            stream.Position = 0;

                            string excelName = $"Users-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                        }
                    }
                }
            }
        }
    }
}
