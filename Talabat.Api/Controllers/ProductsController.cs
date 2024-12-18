using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Dtos;
using Talabat.Api.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;
using Talabat.Repository.Repositories;

namespace Talabat.Api.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        //private readonly IGenericRepository<Product> _generic;
        //private readonly IGenericRepository<ProductBrand> brandrepo;
        //private readonly IGenericRepository<ProductCategory> categoryrepo;
        private readonly IMapper mapper;

        public ProductsController(/*IGenericRepository<Product> generic , IGenericRepository<ProductBrand> brandrepo, IGenericRepository<ProductCategory> categoryrepo,*/IProductService productService, IMapper mapper)
        {
            ////_generic = generic;
            ////this.brandrepo = brandrepo;
            ////this.categoryrepo = categoryrepo;
            _productService = productService;
            this.mapper = mapper;
        }
        [ProducesResponseType(typeof(ProductToReturnDto) , StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProductToReturnDto) , statusCode:404)]
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<PagintationData<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specparam)
        {
           var prods = await _productService.GetProductsAsync(specparam);
            
            var count = await _productService.GetCountAsync(specparam);
            if (!prods.Any())
            {
                return BadRequest();
                //return NotFound(new { Message = "There Is no Products", StatusCode = 404 });
            }
            var mapped = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(prods);
            return Ok(new PagintationData<ProductToReturnDto>(specparam.PageSize , specparam.PageIndex , mapped ,count ));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          var prod = await _productService.GetProductAsync(id);
            if (prod == null)
            {
                
               return NotFound(new { Message = "Not Found Product", StatusCode = 404 });
            }
            return Ok(mapper.Map<Product,ProductToReturnDto>(prod));
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var brands = await _productService.GetBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetCategories()
        {
            var cats = await _productService.GetCategoriesAsync();
            return Ok(cats);
        }


    }
}