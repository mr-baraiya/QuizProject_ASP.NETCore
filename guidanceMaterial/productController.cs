using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;


namespace My_Project_dotNET.Controllers
{
    public class productController : Controller
    {
        private IConfiguration configuration;

        public productController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            String connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Product_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
    }
  }
