using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.UnitOfWork.Contract;
using Product = Talabat.Core.Entities.Product;
namespace Talabat.Services
{
    internal class Paymentservice : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ICartRepository _cartRepository;

        public Paymentservice(IUnitOfWork unitOfWork,IConfiguration configuration , ICartRepository cartRepository)
        {
            this.unitOfWork = unitOfWork;
            _configuration = configuration;
            _cartRepository = cartRepository;
        }
        public async Task<CustomerCart> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            var Basket = await _cartRepository.GetCartAsync(basketId); 
            if(Basket == null) return null;
            var shippingprice = 0m;
            if(Basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.Repositories<DeliveryMethod>().GetAsync(Basket.DeliveryMethodId.Value);
                shippingprice = deliveryMethod.Cost;
                Basket.ShippingCost = deliveryMethod.Cost; 
            }
            if (Basket?.Items?.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await unitOfWork.Repositories<Product>().GetAsync(item.Id);
               if (item.Price!= product.Price) item.Price = product.Price;
                }
            }
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)Basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingprice * 100,
                    Currency = "usd",
                    PaymentMethodTypes  = new List<string>() { "card"}
                }; 
                paymentIntent = await service.CreateAsync(options);
                Basket.PaymentIntentId = paymentIntent.Id;  
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingprice * 100,
                };
                await service.UpdateAsync(Basket.PaymentIntentId , options);
             }
            await _cartRepository.UpdateCartAsync(Basket);
            return Basket;
        }
    }
}
