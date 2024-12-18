using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Api.Controllers
{
    public class BasketsController : BaseController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper mapper;

        public BasketsController(ICartRepository cartRepository ,IMapper mapper)
        {
            _cartRepository = cartRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetBasket(string id)
        {
            var basket = await _cartRepository.GetCartAsync(id);
            return Ok(basket ?? new CustomerCart(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerCart>> UpdateBasket(CustomerCartDto cart)
        {
            var mapped = mapper.Map<CustomerCartDto , CustomerCart>(cart);
            var update = await _cartRepository.UpdateCartAsync(mapped);
            if (update is null) return BadRequest();
            return Ok(update);

        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
             await _cartRepository.DeleteItemsAsync(id);
           
        }
    }
}
