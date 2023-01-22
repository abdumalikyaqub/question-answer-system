using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_qasysapp.Models;
using Test_qasysapp.Models.Repository;
using Test_qasysapp.Models.ViewModels;

namespace Test_qasysapp.Controllers
{
    public class QuestionController : Controller
    {

		CategoryRepos category = new CategoryRepos();
		QuestionRepos questionRepos = new QuestionRepos();
        AnswerRepos  answerRepos = new AnswerRepos();
        VoteRepos voteRepos = new VoteRepos();
		UserRepos users = new UserRepos();
		QuestionDetailsViewModel detailsViewModel = new QuestionDetailsViewModel();

        // GET: QuestionController
        [Authorize]
		public ActionResult Index()
        {
            //var userItem = users.GetUsers();
            //bool isuser = userItem.Any(u => u.Id == id);

            //if (isuser)
            //{
            //    var getuser = userItem.First(u => u.Id == id);
            //    ViewBag.User_ID = getuser.Id;
                //ViewBag.Users = users.GetUsers();
                ViewBag.Cats = category.GetCategories();

                ModelState.Clear();
                return View(questionRepos.GetQuestions());
            //}
            //return RedirectToAction("Index", "Auth");
        }

        public ActionResult MyQuestion(int userid)
        {
            List<Question> questionList = new List<Question>();
            foreach (var item in questionRepos.GetQuestions())
            {
                if (item.AuthorId == userid)
                {
                    questionList.Add(item);
                }
            }
            return View(questionList);
        }

        // GET: QuestionController/Details/5
        public ActionResult Details(int id)
        {
            // Question question = new Question();
            //ViewBag.User_ID = useid;
            if (questionRepos.GetQuestions == null)
            {
                return NotFound();
            }

            //ViewBag.DetailId = id;

            // var modeledit = questionRepos.GetQuestions().Where(p => p.Id == id);

            foreach (var item in questionRepos.GetQuestions())
            {
                if (item.Id == id)
                {
                    detailsViewModel.Question = item;
                }
            }

            if (detailsViewModel.Question == null)
            {
                return NotFound();
            }

            foreach (var item in answerRepos.GetAnswers())
            {
                if (item.QuestionId == id)
                {
                    detailsViewModel.AnswersList.Add(item);
                }
            }

            ViewBag.User = users.GetUsers();

            ViewBag.Votes = voteRepos.GetVotes();

            return View(detailsViewModel);
        }

        [HttpPost]
        public ActionResult CreateAnswer(Answer answer)
        {
            if (answerRepos.AddAnswer(answer))
            {
                //ViewBag.Message = "Added Successfully";
                ModelState.Clear();
                return RedirectToAction(nameof(Details), new { id = answer.QuestionId });
            }

			return RedirectToAction(nameof(Details), new { id = answer.QuestionId });
		}

        [HttpPost]
        public ActionResult AddVoteStatus(Vote vote)
        {
           /* if (!ModelState.IsValid)
            {
                return BadRequest("Error in valid");
            }*/
            /*if (voteRepos.AddVoteStatus(vote))
			{
				
				ModelState.Clear();
				return RedirectToAction(nameof(Details), new { });
			}
            */
            voteRepos.AddVote(vote);

			return Ok("Successfuly");
		}

        // GET: QuestionController/Create
        public ActionResult Create()
        {
            //UserRepos users = new UserRepos();
            ViewBag.User = users.GetUsers();

            ViewBag.Cats = category.GetCategories();

            return View();
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
           /* try
            {
               */
               /* if (ModelState.IsValid)
                {*/
                    //QuestionRepos ques = new QuestionRepos();
                    if (questionRepos.AddQuestion(question))
                    {
                        //ViewBag.Message = "Added Successfully";
                        ModelState.Clear();
                        return RedirectToAction(nameof(Index));
                    }
                    
                //}
                return View();
            /* 
             return View();
         }
         catch
         {
             return View();
         }*/

        }

        // GET: QuestionController/Edit/5
        public ActionResult Edit(int id)
        {
            Question question = new Question();
            if ( questionRepos.GetQuestions == null)
            {
                return NotFound();
            }

           // var modeledit = questionRepos.GetQuestions().Where(p => p.Id == id);

            foreach (var item in questionRepos.GetQuestions())
            {
                if (item.Id == id)
                {
                    question = item;
                }
            }

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Question question)
        {
            try
            {
                if (questionRepos.Update(question))
                {
                    int usid = Convert.ToInt32(User.Identity.Name);
                    ModelState.Clear();
                    return RedirectToAction(nameof(MyQuestion), new {userid = usid});
                }
               
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: QuestionController/Delete/5
        public ActionResult Delete(int id)
        {
            Question question = new Question();
            if (questionRepos.GetQuestions == null)
            {
                return NotFound();
            }

            // var modeledit = questionRepos.GetQuestions().Where(p => p.Id == id);

            foreach (var item in questionRepos.GetQuestions())
            {
                if (item.Id == id)
                {
                    question = item;
                }
            }

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: QuestionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Question question)
        {
            
                if (questionRepos.Delete(id))
                {
                    int usid = Convert.ToInt32(User.Identity.Name);
                    return RedirectToAction(nameof(MyQuestion), new { userid = usid });
				}
            
            return View();
        }
    }
}