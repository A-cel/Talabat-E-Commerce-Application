
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class CountWithSpec : BaseSpecification<Product>
    {
        public CountWithSpec(ProductSpecParams prodspecParams):base((p) =>
        (!prodspecParams.BrandId.HasValue || p.BrandId == prodspecParams.BrandId.Value) &&
        (!prodspecParams.CategoryId.HasValue || p.CategoryId == prodspecParams.CategoryId.Value))
        {
            
        }
    }
}
