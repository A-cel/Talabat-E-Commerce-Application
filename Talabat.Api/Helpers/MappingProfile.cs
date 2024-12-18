using AutoMapper;
using Talabat.Api.Dtos;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;

namespace Talabat.Api.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductToReturnDto>().ForMember(d=>d.Brand , o=>o.MapFrom(p=>p.Brand.Name))
                .ForMember(d=>d.Category , o=>o.MapFrom(p=>p.Category.Name))
                .ForMember(d=>d.PictureUrl , o=>o.MapFrom<PictureUrlResolver>());
            CreateMap<CustomerCartDto, CustomerCart>();
            CreateMap<CartItemDto, CartItem>();
            CreateMap<AddressDto, Core.Entities.Order_Aggregate.Address>();
            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
                .ForMember(d=>d.ShippingAddress , o=>o.MapFrom(s=>s.ShippingAdderss));
            //.ForMember(d=>d.Status , o=>o.MapFrom(s=>s.Status));
            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
            .ForMember(d=>d.PictureUrl , o=>o.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
