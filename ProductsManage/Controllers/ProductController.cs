using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsManage.Data;
using ProductsManage.Models;

namespace ProductsManage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public async Task<ActionResult<PagedResult<ProductResponseDto>>> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10,
        [FromQuery] string? search = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] string? status = null)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(p => p.Status == status);

            var total = await query.CountAsync();
            var items = await query.OrderBy(p => p.Id).Skip((page - 1) * limit).Take(limit).ToListAsync();

            var dtos = _mapper.Map<List<ProductResponseDto>>(items);

            var result = new PagedResult<ProductResponseDto>
            {
                Items = dtos,
                Total = total,
                Page = page,
                Limit = limit,
                TotalPages = (int)Math.Ceiling(total / (double)limit)
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return Ok(_mapper.Map<ProductResponseDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct(ProductCreateDto dto)
        {

            var product = _mapper.Map<Product>(dto);
            product.CreatedAt = product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<ProductResponseDto>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _mapper.Map(dto, product);
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ProductResponseDto>(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Delete successfully" });
        }

    }
}
