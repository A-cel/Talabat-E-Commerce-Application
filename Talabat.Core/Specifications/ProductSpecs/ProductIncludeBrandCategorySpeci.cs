using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;

namespace Talabat.Repository.Repositories
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams prodspecParams):base((p)=> 
        (string.IsNullOrEmpty(prodspecParams.Search) || p.Name.ToLower().Contains(prodspecParams.Search.ToLower()))&&
        (!prodspecParams.BrandId.HasValue || p.BrandId == prodspecParams.BrandId.Value)&&
        (!prodspecParams.CategoryId.HasValue || p.CategoryId == prodspecParams.CategoryId.Value))
        {
            AddIncludes();
            SortingType(prodspecParams.Sort);
            ApplyPagination((prodspecParams.PageIndex - 1) * prodspecParams.PageSize, prodspecParams.PageSize);
       
        }
        public ProductSpecification(int id) : base(p=>p.Id == id)
        {
            AddIncludes();
        }
        private void AddIncludes() { 
       Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
        private void SortingType(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "NameDesc":
                        OrderByDesc = p => p.Name;
                        break;
                    case "PriceAsc":
                        OrderByAsc = p => p.Price;
                        break;
                    case "PriceDesc":
                        OrderByDesc = p => p.Price;
                        break ;
                    default:
                        OrderByAsc = p=>p.Name;
                        break;
                }
            }
            else { OrderByAsc = p => p.Name; }
        }
    
    }
}
