using ExaminationProject.Data;
using ExaminationProject.Models;
using ExaminationProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ExaminationProject.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM vm = new()
            {
                ExamCategories = _context.ExamCategories.Where(x => x.IsDeleted == false).ToList(),
            };
            
            return View(vm);
        }

        [HttpGet("examcategory/{id}")]
        [Authorize(Policy = "IsNotDeletedPolicy")]
        public IActionResult ExamCategory(int id)
        {
            var questions = _context.Questions.Where(x => x.ExamCategoryId == id).Where(x => x.IsDeleted == false).ToList();
            var questionIds = questions.Select(x => x.Id).ToList();
            var questionAnswers = _context.QuestionAnswers
                .Include(x => x.Answer)
                .Where(x => questionIds.Contains(x.QuestionId))
                .ToList();
            var answers = questionAnswers.Select(x => x.Answer).Distinct().ToList();
            var categoryName = _context.ExamCategories.Where(x => x.Id == id).First().CategoryName;
            var vm = new ExamCategoryVM
            {
                SelectedCategoryId = id,
                Questions = questions,
                Answers = answers,
                QuestionAnswers = questionAnswers,
                CategoryName = categoryName,
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}