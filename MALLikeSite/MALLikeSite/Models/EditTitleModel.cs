using FluentValidation;
using Models;

namespace MALLikeSite.Models
{
    public class EditTitleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int UserRating { get; set; }
        public decimal AverageRating { get; set; }
        public List<Character> Characters { get; set; }
        public List<Staff> Staffs { get; set; }

        public bool IsAproved;

    }

    public class EditTitleModelValidator : AbstractValidator<EditTitleModel>
    {
        public EditTitleModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 150);
            RuleFor(x => x.Genre).NotEmpty().Length(4, 25);
            RuleFor(x => x.Description).MaximumLength(1500);
            RuleFor(x => x.ReleaseDate).LessThan(d => DateTime.Now)
                .WithMessage("Date must be current date or less")
                .GreaterThan(d => new DateTime(1900,1,1,1,1,1))
                .WithMessage("Date must be greater than 1,1,1900");
        }
    }
}
