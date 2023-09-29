using FluentValidation;
using Models;

namespace MALLikeSite.Models
{
    public class EditStaffModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public List<Title> Titles { get; set; }
        public string Description { get; set; }

        public bool IsAproved;

    }

    public class EditStaffModelValidator : AbstractValidator<EditStaffModel>
    {
        public EditStaffModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 150);
            RuleFor(x => x.Position).NotEmpty().Length(4, 25);
            RuleFor(x => x.Description).MaximumLength(1500);
        }
    }
}
