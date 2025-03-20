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

        #region Profile
        [Route("/Profile")]
        public IActionResult Profile()
        {
            return View();
        }
        #endregion

        #region ChangePassword

        [HttpGet]
        [Route("/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Please correct the errors.";
                    return View(model);
                }

                // Get the current user ID from the session
                int userId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));

                string connectionString = configuration.GetConnectionString("ConnectionString");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("PR_Change_User_Password", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@CurrentPassword", model.CurrentPassword);
                        command.Parameters.AddWithValue("@NewPassword", model.NewPassword);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)  // ✅ This ensures we check if any row exists
                        {
                            while (reader.Read())
                            {
                                int status = Convert.ToInt32(reader["Status"]);
                                string message = reader["Message"].ToString();

                                if (status == 1)
                                {
                                    TempData["SuccessMessage"] = message;
                                    return RedirectToAction("Profile"); // Redirect to profile page
                                }
                                else
                                {
                                    TempData["ErrorMessage"] = message; // Show error message
                                    return View(model);
                                }
                            }
                        }
                        else
                        {
                            // ✅ If no rows returned, it means the password is incorrect.
                            TempData["ErrorMessage"] = "Current password is incorrect.";
                            return View(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }

            return View(model);
        }

        #endregion

        #region UserList
        [HttpGet]
        [Route("ShowUsers")]
        public IActionResult UserList()
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
                        command.CommandText = "PR_MST_User_SelectAll";

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
                TempData["ErrorMessage"] = "An error occurred while fetching the user list: " + ex.Message;
                return RedirectToAction("UserList");
            }
        }
        #endregion

        #region UserDelete
        [HttpGet]
        public IActionResult UserDelete(int UserID)
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
                        command.CommandText = "PR_MST_User_Delete";
                        command.Parameters.Add("@userID", SqlDbType.Int).Value = UserID;

                        command.ExecuteNonQuery();
                    }
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
        #endregion

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            try
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
                    TempData["SuccessMessage"] = "Registration successful. Please log in.";
                    return RedirectToAction("Login");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while registering the user: " + ex.Message;
                return View(model);
            }
        }
        #endregion

        #region UserLogin

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                string ErrorMsg = string.Empty;

                if (string.IsNullOrEmpty(userLoginModel.UserName))
                {
                    ErrorMsg += "User Name is Required";
                }

                if (string.IsNullOrEmpty(userLoginModel.Password))
                {
                    ErrorMsg += "<br/>Password is Required";
                }

                if (!string.IsNullOrEmpty(ErrorMsg))
                {
                    TempData["ErrorMessage"] = ErrorMsg;
                    return RedirectToAction("Login", "User");
                }

                if (ModelState.IsValid)
                {
                    using (SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString")))
                    {
                        conn.Open();
                        using (SqlCommand objCmd = conn.CreateCommand())
                        {
                            objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            objCmd.CommandText = "PR_MST_User_Login";
                            objCmd.Parameters.AddWithValue("@UserName", userLoginModel.UserName);
                            objCmd.Parameters.AddWithValue("@Password", userLoginModel.Password);

                            using (SqlDataReader objSDR = objCmd.ExecuteReader())
                            {
                                if (!objSDR.HasRows)
                                {
                                    TempData["ErrorMessage"] = "Invalid User Name or Password";
                                    return RedirectToAction("Login", "User");
                                }

                                DataTable dtLogin = new DataTable();
                                dtLogin.Load(objSDR);

                                foreach (DataRow dr in dtLogin.Rows)
                                {
                                    HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                                    HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                                    HttpContext.Session.SetString("Mobile", dr["Mobile"].ToString());
                                    HttpContext.Session.SetString("Email", dr["Email"].ToString());
                                    HttpContext.Session.SetString("Password", dr["Password"].ToString());
                                }
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = ErrorMsg;
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while logging in: " + ex.Message;
                return RedirectToAction("Login", "User");
            }
        }

        #endregion

        #region Logout
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while logging out: " + ex.Message;
                return RedirectToAction("Login");
            }
        }
        #endregion

    }
}
