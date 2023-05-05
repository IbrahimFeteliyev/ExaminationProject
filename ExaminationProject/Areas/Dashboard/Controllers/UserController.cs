using ExaminationProject.Areas.Dashboard.ViewModels;
using ExaminationProject.Data;
using ExaminationProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web.Helper;

namespace ExaminationProject.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]

    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;


        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor, IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        public IActionResult Index()
        {

            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Groups"] = _context.Groups.Where(x => x.IsDeleted == false).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user, IFormFile NewPhoto, int groupId)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123123Az@");
            user.CreatedDate = DateTime.Now;

            user.PhotoUrl = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
            _userManager.CreateAsync(user);
            var gr = _context.Groups.FirstOrDefault(x => x.Id == groupId);
            UserGroup userGroup = new()
            {
                UserId = user.Id,
                GroupId = gr.Id,
            };
            _context.UserGroups.Add(userGroup);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            if (id == null) return NotFound();

            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
            var roles = _roleManager.Roles.Select(x => x.Name).ToList();

            UserRoleAddViewModel vm = new()
            {
                User = user,
                Roles = roles.Except(userRoles)
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string id, string role)
        {
            if (id == null) return NotFound();

            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var userAddRole = await _userManager.AddToRoleAsync(user, role);

            if (!userAddRole.Succeeded)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(User user, string id)
        {
            //ViewData["User"] = _context.Users.Where(x => x.Id == user.Id);
            var usr = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            var group = _context.Groups.ToList();
            var userGroup = _context.UserGroups.Where(x => x.UserId == user.Id).ToList();

            UserEditVM editVM = new()
            {
                Users = usr,
                Groups = group,
                UserGroups = userGroup,
            };
            return View(editVM);
        }

        [HttpPost]
        public IActionResult Edit(User user, IFormFile NewPhoto, string OldPhoto, int groupId)
        {
            try
            {
                if (NewPhoto != null)
                {
                    user.PhotoUrl = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
                }
                else
                {
                    user.PhotoUrl = OldPhoto;
                }
                user.UpdatedDate = DateTime.Now;
                _context.Users.Update(user);

                var userGroup = _context.UserGroups.Where(x => x.UserId == user.Id).ToList();
                _context.UserGroups.RemoveRange(userGroup);
                _context.SaveChanges();
                var gr = _context.Groups.FirstOrDefault(x => x.Id == groupId);
                UserGroup ug = new()
                {
                    UserId = user.Id,
                    GroupId = gr.Id,
                };
                _context.UserGroups.Add(ug);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
      
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            try
            {
                var us = await _context.Users.SingleOrDefaultAsync(x => x.Id == user.Id);
                us.IsDeleted = true;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> UserInfo()
        //{
        //    var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    User user = await _userManager.FindByIdAsync(userId);

        //    return View(user);
        //}

    }
}
