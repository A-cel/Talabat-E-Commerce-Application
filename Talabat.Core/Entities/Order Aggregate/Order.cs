using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order :BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAdderss, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAdderss = shippingAdderss;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderState Status { get; set; } = OrderState.Pending;
        public Address ShippingAdderss { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal Subtotal { get; set; }
        public decimal GetTotal()=> Subtotal + DeliveryMethod.Cost;
        //public string PaymentIntent { get; set; }
    }
}
