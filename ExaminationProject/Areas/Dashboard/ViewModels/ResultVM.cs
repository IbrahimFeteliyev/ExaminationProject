using ExaminationProject.Models;

namespace ExaminationProject.Areas.Dashboard.ViewModels
{
    public class ResultVM
    {
        public List<User> Users { get; set; }
        public List<ExamResult> ExamResults { get; set; }
        public List<ExamCategory> ExamCategories { get; set; }
    }
}
