using ExaminationProject.Models;

namespace ExaminationProject.Areas.Dashboard.ViewModels
{
    public class UserEditVM
    {
        public User Users { get; set; }
        public List<Group> Groups { get; set; }
        public List<UserGroup> UserGroups { get; set; }
    }
}
