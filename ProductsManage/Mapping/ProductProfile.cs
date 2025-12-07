using AutoMapper;
using ProductsManage.Models;

namespace ProductsManage.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s =>
                    s.Category != null ? new CategoryDto { Id = s.Category.Id, Name = s.Category.Name } : null));

            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
