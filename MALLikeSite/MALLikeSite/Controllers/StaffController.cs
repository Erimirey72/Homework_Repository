using MALLikeSite.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using FluentValidation;
using BusinessLogic;
using Models;
using BusinessLogic.Services;


namespace MALLikeSite.Controllers
{
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly IStaffService _staffService;
        private readonly IValidator<CreateStaffModel> _createStaffValidator;
        private readonly IValidator<EditStaffModel> _editStaffValidator;

        public StaffController(ILogger<StaffController> logger, ApplicationDbContext application, IStaffService staffService, IValidator<CreateStaffModel> createStaffValidator, IValidator<EditStaffModel> editStaffValidator)
        {
            _logger = logger;
            _staffService = staffService;
            _createStaffValidator = createStaffValidator;
            _editStaffValidator = editStaffValidator;
        }

        private static List<Staff> staffs = new List<Staff>();

        public IActionResult StaffPage()
        {
            var model = new StaffPageViewModel();

            var staffs = _staffService.GetAll();

            model.Staffs = staffs;

            return View("StaffPage", model);
        }

        [HttpGet("createstaff")]
        public IActionResult Create()
        {
            return View();
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
                ModelState.AddModelError("", "Invalid input");
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
            existingStaff.Position = model.Position;
            existingStaff.Titles = model.Titles;

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