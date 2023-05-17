using ExaminationProject.Areas.Dashboard.ViewModels;
using ExaminationProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExaminationProject.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ExamResultController : Controller
    {
        private readonly AppDbContext _context;

        public ExamResultController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ResultVM vm = new() 
            { 
                Users = _context.Users.ToList(),
                ExamCategories = _context.ExamCategories.ToList(),
                ExamResults = _context.ExamResults.ToList(),
            };

            return View(vm);
        }
    }
}
