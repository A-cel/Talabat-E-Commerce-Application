using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.ProductSpecs;
using Talabat.Core.UnitOfWork.Contract;
using Talabat.Repository.Repositories;

namespace Talabat.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.Repositories<ProductBrand>().GetAllAsync();
        return brands;
        }

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        { 
             var categories = await _unitOfWork.Repositories<ProductCategory>().GetAllAsync();
            return categories;
        }

        public async Task<int> GetCountAsync(ProductSpecParams specparam)
        {
            var spec = new ProductSpecification(specparam);
            var prodcount = await _unitOfWork.Repositories<Product>().CountWithSpecAsync(spec);
                 return prodcount;
        }

        public async Task<Product?> GetProductAsync(int productid)
        {
            var spec = new ProductSpecification(productid);
            var prod =await  _unitOfWork.Repositories<Product>().GetWithSpec(spec);
            return prod;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specparam)
        {
            var spec = new ProductSpecification(specparam);
            var repository =  _unitOfWork.Repositories<Product>();
            var prods = await repository.GetAllWithSpec(spec);
            return prods;
        }
    }
}
