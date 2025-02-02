using Microsoft.AspNetCore.Mvc;

namespace My_Project_dotNET.Controllers
{
    public class QuestionLevelController : Controller
    {
        public IActionResult QuestionLevelList()
        {
            return View();
        }

        public IActionResult AddQuestionLevel()
        {
            return View();
        }
    }
}
