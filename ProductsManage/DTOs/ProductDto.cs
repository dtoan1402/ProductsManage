namespace ProductsManage.Models
{
    public class ProductCreateDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required int StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public required string Status { get; set; }
    }

    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public string? Status { get; set; }
    }
    public class ProductResponseDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public CategoryDto? Category { get; init; }
        public required string Status { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }

    public class CategoryDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
    }
}
