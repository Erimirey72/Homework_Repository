using FluentValidation;
using MALLikeSite.Models;
using Models;

public class EditCharacterModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Title> Titles { get; set; }
    public List<Staff> VoiceActor { get; set; }
    public string Description { get; set; }

    public bool IsAproved;

}
public class EditCharacterModelValidator : AbstractValidator<EditCharacterModel>
{
    public EditCharacterModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(1, 150);
        RuleFor(x => x.Description).MaximumLength(1500);
    }
}