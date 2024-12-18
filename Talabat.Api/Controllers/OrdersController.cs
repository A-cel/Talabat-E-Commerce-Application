using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Api.Dtos;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;
using Talabat.Core.UnitOfWork.Contract;

namespace Talabat.Api.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork , IOrderService orderService ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);
            var mapaddress = _mapper.Map<AddressDto, Address>(orderDto.shippingaddress);
          var createdorder =   await _orderService.CreateOrderAsync(buyerEmail: buyeremail, address: mapaddress,
                deliverymethodId: orderDto.Deliverymethodid, basketId: orderDto.basketid);
            if (createdorder is null) return BadRequest();
            return Ok(createdorder);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);
            //var mappedorder =  _mapper.Map<IReadOnlyList<Order> , IReadOnlyList<OrderToReturnDto>>(orders);
            return Ok(_mapper.Map< IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id )
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdForUserAsync(id , buyerEmail);
            if(order is null) return NotFound();
            //var mappedorder =  _mapper.Map<IReadOnlyList<Order> , IReadOnlyList<OrderToReturnDto>>(orders);
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var result = await _orderService.GetDeliveryMethodsAsync();
            if (result is null) return NotFound();
            return Ok(_mapper.Map<IReadOnlyList<DeliveryMethodDto>>(result));
            //return Ok( await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
 