using homework3_26.Data;
using homework3_26.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace homework3_26.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            var repo = new QuestionsRepository(_connectionString);
            HomeViewModel vm = new HomeViewModel();
            vm.Questions = repo.GetQuestions();
          
            return View(vm);
        }

        public IActionResult ViewQuestion(int id)
        {
            var repo = new QuestionsRepository(_connectionString);
            QuestionViewModel vm = new QuestionViewModel();
            vm.Question = repo.GetQuestion(id);
            if (User.Identity.IsAuthenticated)
            {
                var user = repo.GetByEmail(User.Identity.Name);
                vm.User = user;
            }
            return View(vm);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var repo = new QuestionsRepository(_connectionString);
            var user = repo.Login(email, password);
            if (user == null)
            {
                return RedirectToAction("Login");
            }


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Cookies", ClaimTypes.Email, "roles"))
                ).Wait();

            return Redirect("/home/index");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User user, string password)
        {
            var repo = new QuestionsRepository(_connectionString);
            repo.AddUser(user, password);
            return Redirect("/home/login");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }

        [Authorize]
        public IActionResult AskAQuestion()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Question question, string tags)
        {
            var repo = new QuestionsRepository( _connectionString);
            question.Date = DateTime.Now;
            var user = repo.GetByEmail(User.Identity.Name);
            question.UserId = user.Id;
            repo.AddQuestion(question, tags);
            return Redirect("/");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddAnswer(Answer answer, int questionId)
        {
            var repo = new QuestionsRepository(_connectionString);
            answer.Date = DateTime.Now;
            var user = repo.GetByEmail(User.Identity.Name);
            answer.UserId = user.Id;
            answer.QuestionId = questionId;
            repo.AddAnswer(answer);
            return Redirect($"/home/viewquestion?id={questionId}");
        }
    }
}   