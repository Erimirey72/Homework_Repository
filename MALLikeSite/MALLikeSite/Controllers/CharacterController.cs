using MALLikeSite.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using FluentValidation;
using BusinessLogic;
using Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class CharacterController : Controller
{
    private readonly ILogger<CharacterController> _logger;
    private readonly ICharacterService _characterService;
    private readonly ITitleService _titleService;
    private readonly IStaffService _staffService;
    private readonly IValidator<CreateCharacterModel> _createCharacterValidator;
    private readonly IValidator<EditCharacterModel> _editCharacterValidator;

    public CharacterController(ILogger<CharacterController> logger, ApplicationDbContext application, ICharacterService characterService, ITitleService titleService, IStaffService staffService, IValidator<CreateCharacterModel> createCharacterValidator, IValidator<EditCharacterModel> editCharacterValidator)
    {
        _logger = logger;
        _characterService = characterService;
        _createCharacterValidator = createCharacterValidator;
        _editCharacterValidator = editCharacterValidator;
        _titleService = titleService;
        _staffService = staffService;
    }

    private static List<Character> characters = new List<Character>();

    public IActionResult CharacterPage()
    {
        var model = new CharacterPageViewModel();

        var characters = _characterService.GetAll();

        model.Characters = characters;

        return View("StaffPage", model);
    }

    [HttpGet("createcharacter")]
    public IActionResult Create()
    {
        var model = new CreateCharacterModel
        {
            StaffsSelectList = _staffService.GetAll().Select(staff =>
                    new SelectListItem { Value = staff.Id.ToString(), Text = staff.Name }).ToList(),
            TitlesSelectList = _titleService.GetAll().Select(title =>
                    new SelectListItem { Value = title.Id.ToString(), Text = title.Name }).ToList()
        };
        return View(model);
    }

    [HttpPost("createcharacter")]
    public IActionResult Create([FromForm] CreateCharacterModel character)
    {
        var validationResult = _createCharacterValidator.Validate(character);
        if (validationResult.IsValid)
        {
            try
            {
                var newCharacterId = _characterService.Create(new Character
                {
                    Id = Guid.NewGuid(),
                    Name = character.Name,
                    Description = character.Description,
                    Titles = character.Titles,
                    VoiceActor = character.VoiceActor,
                    IsApproved = character.IsApproved == false
                });

                return RedirectToAction("CharacterPage", "Home");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Request failed. Request details: {@model}", character);
                throw;
            }
        }
        else
        {
            character.StaffsSelectList = _staffService.GetAll().Select(staff =>
             new SelectListItem { Value = staff.Id.ToString(), Text = staff.Name }).ToList();
            character.TitlesSelectList = _titleService.GetAll().Select(title =>
            new SelectListItem { Value = title.Id.ToString(), Text = title.Name }).ToList();
            return View(character);
        }
    }

    [HttpGet("editcharacter/{id}")]
    public IActionResult Edit(Guid id)
    {
        var character = _characterService.GetById(id);

        var characterModel = new EditCharacterModel
        {
            Id = id,
            Name = character.Name,
            Description = character.Description,
            Titles = character.Titles,
            VoiceActor = character.VoiceActor,
            IsApproved = character.IsApproved
        };

        return View(characterModel);
    }

    [HttpPost("editcharacter/{id}")]
    public IActionResult Edit(Guid id, [FromForm] EditCharacterModel model)
    {
        var existingCharacter = _characterService.GetById(id);
        var validationResult = _editCharacterValidator.Validate(model);

        existingCharacter.Name = model.Name;
        existingCharacter.Description = model.Description;
        existingCharacter.Titles = model.Titles;
        existingCharacter.VoiceActor = model.VoiceActor;
        existingCharacter.IsApproved = model.IsApproved == false;

        _characterService.Edit(existingCharacter);

        return RedirectToAction("CharacterPage", "Home");
    }

    [HttpGet("deletecharacter/{id}")]
    public IActionResult Delete(Guid id)
    {
        var character = _characterService.GetById(id);

        return View(character);
    }

    [HttpPost("deletecharacter/{id}")]
    public IActionResult ConfirmDeletion(Guid id)
    {
        try
        {
            _characterService.DeleteById(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Request failed. Request details: {id}", id);
        }

        return RedirectToAction("CharacterPage", "Home");
    }

    [HttpGet("Character/{id}")]
    public IActionResult Details(Guid id)
    {
        var character = _characterService.GetById(id);

        var characterModel = new CharacterViewModel
        {
            Id = id,
            Name = character.Name,
            Description = character.Description,
            Titles = character.Titles,
            VoiceActor = character.VoiceActor
        };

        return View(characterModel);
    }
}