using MALLikeSite.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using FluentValidation;
using BusinessLogic;
using Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MALLikeSite.Controllers
{
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly IStaffService _staffService;
        private readonly ITitleService _titleService;
        private readonly IValidator<CreateStaffModel> _createStaffValidator;
        private readonly IValidator<EditStaffModel> _editStaffValidator;

        public StaffController(ILogger<StaffController> logger, ApplicationDbContext application, IStaffService staffService, ITitleService titleService, IValidator<CreateStaffModel> createStaffValidator, IValidator<EditStaffModel> editStaffValidator)
        {
            _logger = logger;
            _staffService = staffService;
            _createStaffValidator = createStaffValidator;
            _editStaffValidator = editStaffValidator;
            _titleService = titleService;
        }

        public IActionResult StaffPage()
        {
            var model = new StaffPageViewModel();

            var staffs = _staffService.GetAll();

            model.Staffs = staffs;

            return View(model);
        }

        [HttpGet("createstaff")]
        public IActionResult Create()
        {
            var model = new CreateStaffModel
            {
                TitlesSelectList = _titleService.GetAll().Select(title =>
                    new SelectListItem { Value = title.Id.ToString(), Text = title.Name }).ToList()
            };
            return View(model);
        }

        [HttpPost("createstaff")]
        public IActionResult Create([FromForm] CreateStaffModel staff)
        {
            var validationResult = _createStaffValidator.Validate(staff);
            if (validationResult.IsValid)
            {
                try
                {
                    var newStaffId = _staffService.Create(new Staff
                    {
                        Id = Guid.NewGuid(),
                        Name = staff.Name,
                        Position = staff.Position,
                        Description = staff.Description,
                        Titles = staff.Titles,
                        IsApproved = staff.IsApproved == false
                    });

                    return RedirectToAction("StaffPage", "Home");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Request failed. Request details: {@model}", staff);
                    throw;
                }
            }
            else
            {
                staff.TitlesSelectList = _titleService.GetAll().Select(title =>
            new SelectListItem { Value = title.Id.ToString(), Text = title.Name }).ToList();
                return View(staff);
            }
        }

        [HttpGet("editstaff/{id}")]
        public IActionResult Edit(Guid id)
        {
            var staff = _staffService.GetById(id);

            var staffModel = new EditStaffModel
            {
                Id = id,
                Name = staff.Name,
                Position = staff.Position,
                Description = staff.Description,
                Titles = staff.Titles,
                IsApproved = staff.IsApproved == false
            };

            return View(staffModel);
        }

        [HttpPost("editstaff/{id}")]
        public IActionResult Edit(Guid id, [FromForm] EditStaffModel model)
        {
            var existingStaff = _staffService.GetById(id);
            var validationResult = _editStaffValidator.Validate(model);

            existingStaff.Name = model.Name;
            existingStaff.Position = model.Position;
            existingStaff.Description = model.Description;
            existingStaff.Titles = model.Titles;
            existingStaff.IsApproved = model.IsApproved = false;

            _staffService.Edit(existingStaff);

            return RedirectToAction("StaffPage", "Home");
        }

        [HttpGet("deletestaff/{id}")]
        public IActionResult Delete(Guid id)
        {
            var staff = _staffService.GetById(id);

            return View(staff);
        }

        [HttpPost("deletestaff/{id}")]
        public IActionResult ConfirmDeletion(Guid id)
        {
            try
            {
                _staffService.DeleteById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Request failed. Request details: {id}", id);
            }

            return RedirectToAction("StaffPage", "Home");
        }

        [HttpGet("Staff/{id}")]
        public IActionResult Details(Guid id)
        {
            var staff = _staffService.GetById(id);

            var staffModel = new StaffViewModel
            {
                Id = id,
                Name = staff.Name,
                Position = staff.Position,
                Description = staff.Description,
                Titles = staff.Titles,
            };

            return View(staffModel);
        }
    }
}
