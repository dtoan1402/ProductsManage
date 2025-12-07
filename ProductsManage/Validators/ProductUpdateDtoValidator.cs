using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductsManage.Data;
using ProductsManage.Models;

namespace ProductsManage.Validators
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        private readonly AppDbContext _context;
        public int ProductId { get; set; }

        public ProductUpdateDtoValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).When(x => x.Price.HasValue);

            RuleFor(x => x.Status)
                .Must(s => new[] { "active", "inactive", "discontinued" }.Contains(s))
                .When(x => x.Status != null);
        }
    }
}
