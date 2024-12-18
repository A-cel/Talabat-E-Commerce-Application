using Talabat.Core.Entities;

namespace Talabat.Api.Dtos
{
    public class CustomerCartDto
    {
        public string Id { get; set; }
        public List<CartItemDto> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
