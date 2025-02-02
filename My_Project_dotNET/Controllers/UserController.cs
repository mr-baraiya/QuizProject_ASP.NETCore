using Microsoft.AspNetCore.Mvc;

namespace My_Project_dotNET.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserList()
        {
            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }
    }
}
