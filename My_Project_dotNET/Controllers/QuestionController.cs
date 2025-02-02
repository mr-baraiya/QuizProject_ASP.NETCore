using Microsoft.AspNetCore.Mvc;

namespace My_Project_dotNET.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult QuestionList()
        {
            return View();
        }

        public IActionResult AddQuestion()
        {
            return View();
        }
    }
}
