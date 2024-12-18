using AutoMapper;
using AutoMapper.Execution;
using Talabat.Api.Dtos;
using Talabat.Core.Entities;

namespace Talabat.Api.Helpers
{
    public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
              var pictureUrl = source.PictureUrl;
            destination.PictureUrl = $"{configuration["ApiBaseUrl"]}/{pictureUrl}";
            if (!string.IsNullOrEmpty( source.PictureUrl)) 
            return $"{configuration["ApiBaseUrl"]}/{source.PictureUrl}";
               
            return string.Empty;
        }
    }
}
