using Stripe;

namespace Talabat.Core.Entities
{
    public class CustomerCart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingCost { get; set; }
        public CustomerCart(string id)
        {
            Id = id;
            Items = new List<CartItem>();
        }

    }
}
