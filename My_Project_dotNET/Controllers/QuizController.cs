using Microsoft.AspNetCore.Mvc;

namespace My_Project_dotNET.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult QuizList()
        {
            return View();
        }

        public IActionResult AddQuiz()
        {
            return View();
        }
    }
}
