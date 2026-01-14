using E_Commerce.Requests.Product;
using FluentValidation;

namespace ControllerFluentValidations.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product Name is required")
            .Length(3, 255).WithMessage("Product Name must be between 3 and 255 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description can't exceed 1000 characters");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Product Price is required")
            .GreaterThanOrEqualTo(0.01m).WithMessage("Price must be at least 0.01");

        RuleFor(x => x.StockQuantity)
            .NotEmpty().WithMessage("Product Stock Quantity is required")
            .GreaterThanOrEqualTo(0).WithMessage("Stock Quantity can't be negative");
    }    
}
