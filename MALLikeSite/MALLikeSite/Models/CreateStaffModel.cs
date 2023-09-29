using FluentValidation;
using Models;

namespace MALLikeSite.Models
{
    public class CreateStaffModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public List<Title> Titles { get; set; }
    }

    public class CreateStaffModelValidator : AbstractValidator<CreateStaffModel>
    {
        public CreateStaffModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 150);
            RuleFor(x => x.Position).NotEmpty().Length(4, 25);
            RuleFor(x => x.Description).MaximumLength(1500);
        }
    }
}
