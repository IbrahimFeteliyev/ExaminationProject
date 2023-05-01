using Microsoft.AspNetCore.Identity;

namespace ExaminationProject.Models
{
    public class User : IdentityUser
    {
        public string Surname { get; set; }
        public string PhotoUrl { get; set; }
    }
}
