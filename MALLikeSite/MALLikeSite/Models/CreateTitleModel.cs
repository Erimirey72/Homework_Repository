using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace MALLikeSite.Models
{
    public class CreateTitleModel
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Character> Characters { get; set; }
        public List<Staff> Staffs { get; set; }
        public Guid SelectedStaffId { get; set; }
        public List<SelectListItem> StaffsSelectList { get; set; }
        public Guid SelectedCharacterId { get; set; }
        public List<SelectListItem> CharactersSelectList { get; set; }
        public bool IsApproved;
    }

    public class CreateTitleModelValidator : AbstractValidator<CreateTitleModel>
    {
        public CreateTitleModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1,150);
            RuleFor(x => x.Genre).NotEmpty().Length(4, 25);
            RuleFor(x => x.Description).MaximumLength(1500);
            RuleFor(x => x.ReleaseDate).LessThan(d => DateTime.Now)
                .WithMessage("Date must be current date or less")
                .GreaterThan(d => new DateTime(1900,1,1,1,1,1))
                .WithMessage("Date must be greater than 1,1,1900");
        }
    }
}
