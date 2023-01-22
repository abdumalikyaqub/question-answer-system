using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_qasysapp.Models;
using Test_qasysapp.Models.Repository;

namespace Test_qasysapp.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepos categoryRepos = new CategoryRepos();
        // GET: CategoryController
        public ActionResult Index()
        {
            return RedirectToAction(nameof(Index), "Admin");
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            Category cat = categoryRepos.GetCategories().First(c=>c.Id == id);

            return View(cat);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (categoryRepos.AddCat(category))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Category cat = categoryRepos.GetCategories().First(c => c.Id == id);
            return View(cat);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                if (categoryRepos.Update(category))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            Category cat = categoryRepos.GetCategories().First(c => c.Id == id);
            return View(cat);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                if (categoryRepos.Delete(id))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
