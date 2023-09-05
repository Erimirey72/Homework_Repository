using MALLikeSite.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using FluentValidation;
using BusinessLogic;
using Models;
using Humanizer.Localisation;
using System.Xml.Linq;

namespace MALLikeSite.Controllers
{
public class TitleController : Controller
{
        private readonly ILogger<TitleController> _logger;
        private readonly ITitleService _titleService;
        private readonly IValidator<CreateTitleModel> _createTitleValidator;
        private readonly IValidator<EditTitleModel> _editTitleValidator;

        public TitleController(ILogger<TitleController> logger, ApplicationDbContext shop, ITitleService titleService, IValidator<CreateTitleModel> createTitleValidator, IValidator<EditTitleModel> editTitleValidator)
        {
            _logger = logger;
            _titleService = titleService;
            _createTitleValidator = createTitleValidator;
            _editTitleValidator = editTitleValidator;
        }

        private static List<Title> titles = new List<Title>();

        public IActionResult Index()
        {
            var model = new IndexViewModel();

            var titles = _titleService.GetAll();

            model.Titles = titles;

            return View("Index", model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
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
                        Staffs = model.Staffs
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Request failed. Request details: {@model}", model);
                }
            }
            else
            {
                throw new ArgumentException("Invalid input");
                return View();
            }

            return Redirect("Title");
        }

        [HttpGet("edit/{id}")]
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
                Staffs = title.Staffs
            };

            return View(titleModel);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(Guid id, [FromForm] EditTitleModel model)
        {
            var validationResult = _editTitleValidator.Validate(model);
            _titleService.Edit(new Title
            {
                Id = id,
                Name = model.Name,
                Genre = model.Genre,
                Description = model.Description,
                ReleaseDate = model.ReleaseDate,
                Characters = model.Characters,
                Staffs = model.Staffs
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var title = _titleService.GetById(id);

            return View(title);
        }

        [HttpPost("Delete/{id}")]
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
                Staffs = title.Staffs,
            };

            return View(titleModel);
        }
    }
}