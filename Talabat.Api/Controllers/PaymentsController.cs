using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;

namespace Talabat.Api.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
        _paymentService = paymentService;
        }
        [HttpPost("basketId")]
        public async Task<ActionResult<CustomerCart>> CreateOrUpdatePaymentIntent(string basketid)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketid);
            if (basket is null) return BadRequest();
            return Ok(basket);
        }
    }
}
