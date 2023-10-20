using FluentValidation;
using MALLikeSite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

public class CreateCharacterModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Title> Titles { get; set; }
    public List<Staff> VoiceActor { get; set; }
    public Guid SelectedStaffId { get; set; }
    public List<SelectListItem> StaffsSelectList { get; set; }
    public Guid SelectedTitleId { get; set; }
    public List<SelectListItem> TitlesSelectList { get; set; }
    public bool IsApproved;
}

public class CreateCharacterModelValidator : AbstractValidator<CreateCharacterModel>
{
    public CreateCharacterModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 150);
        RuleFor(x => x.Description).MaximumLength(1500);
    }
}