using FluentValidation;
namespace Lesson25.Models
{
    public class ProductValidator : AbstractValidator<ProductsModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(2);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        }
    }
}
