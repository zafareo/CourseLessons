using EmployeeMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRoleController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.UserName = user.UserName;
            var userRoles = await _userManager.GetRolesAsync(user);
            return View(userRoles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return RedirectToAction(nameof(DisplayRoles));
        }

        [HttpGet]
        public IActionResult DisplayRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string role)
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
            return RedirectToAction(nameof(DisplayRoles));
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole()
        {
            var users =  await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRole userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            await _userManager.AddToRoleAsync(user, userRole.RoleName);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserRole(string role, string userName)
        {

            var user = await _userManager.FindByNameAsync(userName);

            var result = await _userManager.RemoveFromRoleAsync(user, role);

            return RedirectToAction(nameof(Details), new { userId = user.Id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRole(string role)
        {

            var roleToDelete = await _roleManager.FindByNameAsync(role);
            var result = await _roleManager.DeleteAsync(roleToDelete);

            return RedirectToAction(nameof(DisplayRoles));
        }
    }
}
