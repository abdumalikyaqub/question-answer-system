using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_qasysapp.Models;
using Test_qasysapp.Models.Repository;

namespace Test_qasysapp.Controllers
{
    public class AnswerController : Controller
    {
        private AnswerRepos answerRepos = new AnswerRepos();
        // GET: AnswerController
        public ActionResult Index(int userid)
        {
            List<Answer> answers = new List<Answer>();
            foreach (var item in answerRepos.GetAnswers())
            {
                if (item.AuthorId == userid)
                {
                    answers.Add(item);
                }
            }
            return View(answers);
        }

        // GET: AnswerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnswerController/Create
        public ActionResult Create()
        {
            return View("~/View/Question/Index.cshtml");
        }

        // POST: AnswerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnswerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnswerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnswerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnswerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
