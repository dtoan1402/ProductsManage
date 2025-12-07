using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductsManage.Data;
using ProductsManage.Models;

namespace ProductsManage.Validators
{
    public class ProductCreateDtoValidator: AbstractValidator<ProductCreateDto>
    {
        private readonly AppDbContext _context;

        public ProductCreateDtoValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please input product name")
                .MaximumLength(255);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Status)
                .NotEmpty()
                .Must(s => new[] { "active", "inactive", "discontinued" }.Contains(s))
                .WithMessage("Status must be: active, inactive, discontinued.");
        }

    }
}
