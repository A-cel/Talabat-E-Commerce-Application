using Talabat.Api.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;
using Talabat.Repository.UnitOfWork;
using Talabat.Core.UnitOfWork.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Services;
namespace Talabat.Api.Extensions
{
    public static class ApplicationServices
    {
        public static void AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IProductService), typeof(ProductService));
            Services.AddScoped<ICartRepository, CartRepository>();
            //Services.AddScoped<typ>
            //Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
        }
    }
}
