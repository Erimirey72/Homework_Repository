using MALLikeSite.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using FluentValidation;
using BusinessLogic;
using Models;
using Humanizer.Localisation;
using System.Xml.Linq;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace MALLikeSite.Controllers
{
    public class TitleController : Controller
{
        private readonly ILogger<TitleController> _logger;
        private readonly ITitleService _titleService;
        private readonly IStaffService _staffService;
        private readonly ICharacterService _characterService;
        private readonly IValidator<CreateTitleModel> _createTitleValidator;
        private readonly IValidator<EditTitleModel> _editTitleValidator;
        private readonly UserManager<ApplicationUser> _userManager;

        public TitleController(UserManager<ApplicationUser> userManager, ILogger<TitleController> logger, ApplicationDbContext application, ITitleService titleService, IStaffService staffService, ICharacterService characterService, IValidator<CreateTitleModel> createTitleValidator, IValidator<EditTitleModel> editTitleValidator)
        {
            _logger = logger;
            _titleService = titleService;
            _staffService = staffService;
            _createTitleValidator = createTitleValidator;
            _editTitleValidator = editTitleValidator;
            _characterService = characterService;
            _userManager = userManager;
        }

        private static List<Title> titles = new List<Title>();

        public IActionResult Index()
        {
            var model = new IndexViewModel();

            var titles = _titleService.GetAll();

            model.Titles = titles;

            return View("Index", model);
        }

        [HttpGet("createtitle")]
        public IActionResult Create()
        {
            var model = new CreateTitleModel
            {
                StaffsSelectList = _staffService.GetAll().Select(staff =>
            new SelectListItem { Value = staff.Id.ToString(), Text = staff.Name }).ToList(),
                CharactersSelectList = _characterService.GetAll().Select(сharacter =>
             new SelectListItem { Value = сharacter.Id.ToString(), Text = сharacter.Name }).ToList()
            };

            return View(model);
        }

        [HttpPost("createtitle")]
        public IActionResult Create([FromForm] CreateTitleModel model)
        {
            var validationResult = _createTitleValidator.Validate(model);
            if (validationResult.IsValid)
            {
                try
                {
                    _titleService.Create(new Title
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        Genre = model.Genre,
                        Description = model.Description,
                        ReleaseDate = model.ReleaseDate,
                        Characters = model.Characters,
                        Staffs = model.Staffs,
                        IsApproved = model.IsApproved == false
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Request failed. Request details: {@model}", model);
                }
            }
            else
            {
                model.StaffsSelectList = _staffService.GetAll().Select(staff =>
             new SelectListItem { Value = staff.Id.ToString(), Text = staff.Name }).ToList();
                model.CharactersSelectList = _characterService.GetAll().Select(сharacter =>
             new SelectListItem { Value = сharacter.Id.ToString(), Text = сharacter.Name }).ToList();
                return View(model);
            }

            return Redirect("Title");
        }

        [HttpGet("edittitle/{id}")]
        public IActionResult Edit(Guid id)
        {
            var title = _titleService.GetById(id);

            var titleModel = new EditTitleModel
            {
                Id = id,
                Name = title.Name,
                Genre = title.Genre,
                Description = title.Description,
                ReleaseDate = title.ReleaseDate,
                Characters = title.Characters,
                Staffs = title.Staffs,
                IsApproved = title.IsApproved
            };

            return View(titleModel);
        }

        [HttpPost("edittitle/{id}")]
        public IActionResult Edit(Guid id, [FromForm] EditTitleModel model)
        {
            var existingTitle = _titleService.GetById(id);
            var validationResult = _editTitleValidator.Validate(model);

            existingTitle.Name = model.Name;
            existingTitle.Genre = model.Genre;
            existingTitle.Description = model.Description;
            existingTitle.ReleaseDate = model.ReleaseDate;
            existingTitle.Characters = model.Characters;
            existingTitle.Staffs = model.Staffs;
            existingTitle.IsApproved = false;

            _titleService.Edit(existingTitle);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("deletetitle/{id}")]
        public IActionResult Delete(Guid id)
        {
            var title = _titleService.GetById(id);

            return View(title);
        }

        [HttpPost("deletetitle/{id}")]
        public IActionResult ConfirmDeletion(Guid id)
        {
            try
            {
                _titleService.DeleteById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Request failed. Request details: {id}", id);
            }

            return RedirectToAction("Index", "Title");
        }

        [HttpGet("Title/{id}")]
        public IActionResult Details(Guid id)
        {
            var title = _titleService.GetById(id);

            var titleModel = new TitleViewModel
            {
                Id = id,
                Name = title.Name,
                Genre = title.Genre,
                Description = title.Description,
                ReleaseDate = title.ReleaseDate,
                UserRating = title.UserRating,
                AverageRating = title.AverageRating,
                Characters = title.Characters,
                Staffs = title.Staffs
            };

            return View(titleModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddToMyList(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var title = _titleService.GetById(id);
            if (title == null)
            {
                return NotFound();
            }

            user.MyTitles ??= new List<Title>();
            user.MyTitles.Add(title);
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }
    }
}
