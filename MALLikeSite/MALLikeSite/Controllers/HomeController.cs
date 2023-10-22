using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using MALLikeSite.Data;
using MALLikeSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MALLikeSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITitleService _titleService;
        private readonly IStaffService _staffService;
        private readonly ICharacterService _characterService;

        public HomeController(ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ITitleService titleService, IStaffService staffService, ICharacterService characterService)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _titleService = titleService;
            _staffService = staffService;
            _characterService = characterService;
        }

        public async Task<IActionResult> Index()
        {
            var adminID = await EnsureUser("qwerty123QWERTY`", "admin@contoso.com");
            await EnsureRole(adminID, "Admin");

            var model = new IndexViewModel();

            var titles = _titleService.GetAll();

            model.Titles = titles;
            return View(model);
        }

        public IActionResult StaffPage()
        {
            var model = new StaffPageViewModel();

            var staffs = _staffService.GetAll();

            model.Staffs = staffs;

            return View(model);
        }

        public IActionResult CharacterPage()
        {
            var model = new CharacterPageViewModel();

            var characters = _characterService.GetAll();

            model.Characters = characters;

            return View(model);
        }
        public async Task<IActionResult> MyList()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var myTitles = currentUser.MyTitles;

            var myListViewModel = new MyListViewModel
            {
                MyTitles = myTitles
            };

            return View(myListViewModel);
        }
        public async Task<IActionResult> ApprovePage()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var titles = _titleService.GetUnapproved();
            var staffs = _staffService.GetUnapproved();
            var characters = _characterService.GetUnapproved();

            var approvePageViewModel = new ApprovePageViewModel
            {
                Item1 = titles,
                Item2 = staffs,
                Item3 = characters
            };

            return View(approvePageViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task<IdentityResult> EnsureRole(string uid, string role)
        {
            if (_roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await _roleManager.RoleExistsAsync(role))
            {
                IR = await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var user = await _userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await _userManager.AddToRoleAsync(user, role);

            return IR;
        }

        private async Task<string> EnsureUser(string testUserPw, string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }
    }
}