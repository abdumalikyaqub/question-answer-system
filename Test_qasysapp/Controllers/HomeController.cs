using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test_qasysapp.Models;
using Test_qasysapp.Models.Repository;

namespace Test_qasysapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QuestionRepos questionRepos = new QuestionRepos();
        UserRepos users = new UserRepos();
        CategoryRepos category = new CategoryRepos();

        public HomeController(ILogger<HomeController> logger)
        {
           // _questionRepos = repos;
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            
            var userItem = users.GetUsers();
            bool isuser = userItem.Any(u=>u.Id == id);

            if (isuser)
            {
                var getuser = userItem.First(u => u.Id == id);
                ViewBag.CurUser = getuser.FullName;
                ViewBag.Users = users.GetUsers();
                ViewBag.Cats = category.GetCategories();

                ModelState.Clear();
                return View(questionRepos.GetQuestions());
            }
            return RedirectToAction("Index", "Auth");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}