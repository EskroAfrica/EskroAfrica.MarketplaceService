using AutoMapper;
using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.DTOs.Response;
using EskroAfrica.MarketplaceService.Domain.Entities;
using Newtonsoft.Json;

namespace EskroAfrica.MarketplaceService.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(p => p.Images, src => src.MapFrom(req => JsonConvert.DeserializeObject<List<string>>(req.Images)));
            CreateMap<ProductRequest, Product>()
                .ForMember(p => p.Images, src => src.MapFrom(req => JsonConvert.SerializeObject(req.Images)));

            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>();

            CreateMap<SubCategoryRequest, SubCategory>();
            CreateMap<SubCategory, SubCategoryResponse>();

            CreateMap<Delivery, DeliveryRequest>();
        }
    }
}
