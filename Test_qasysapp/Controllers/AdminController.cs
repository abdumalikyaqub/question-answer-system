using ArrayToPdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test_qasysapp.Models;
using Test_qasysapp.Models.Repository;
using System.IO;
using System.Text;

namespace Test_qasysapp.Controllers
{
    public class AdminController : Controller
    {
        private QuestionRepos questionRepos = new QuestionRepos();
        private CategoryRepos categoryRepos = new CategoryRepos();
        

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Questions()
        {
            var allquestions = questionRepos.GetQuestions();
            return View(allquestions);
        }

        public IActionResult Search(string data, string pattern)
        {
            var d = data.ToLower();
            //var allquestions = questionRepos.GetQuestions();
            //List<Question> ques = new List<Question>();
            IEnumerable<Question> ques = new List<Question>(); 

            if (pattern == "title")
            {
                ques = questionRepos.GetQuestions().Where(t => t.Title.ToLower().Contains(d));
            }
            else if (pattern == "category")
            {
                var cat = categoryRepos.GetCategories().First(c=> c.Name.ToLower().Contains(d));
                ques = questionRepos.GetQuestions().Where(q => q.CategoryId == cat.Id);
            }

            return View(ques);
        }

        public string ExpToPdf()
        {
            var q = questionRepos.GetQuestions();
            //byte[] pdf = q.ToPdf();
           // File.WriteAllBytes("result.pdf", pdf);


            string path = @"C:\app\note.pdf";   // путь к файлу

           // string text = "Hello METANIT.COM"; // строка для записи

            // запись в файл
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                // byte[] buffer = Encoding.Default.GetBytes(text);
                byte[] pdf = q.ToPdf();
                // запись массива байтов в файл
                fstream.Write(pdf, 0, pdf.Length);
                Console.WriteLine("Текст записан в файл");
            }

            return "Успешно!";
        }

        public IActionResult Categories()
        {
            var allcats = categoryRepos.GetCategories();
            return View(allcats);
        }
    }
}
