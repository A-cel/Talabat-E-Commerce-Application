using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderWithSpec : BaseSpecification<Order>
    {
        public OrderWithSpec( string buyeremail ):base(O=>(O.BuyerEmail == buyeremail))
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o=>o.Items);
            AddOrderByDesc(o=>o.OrderDate);
        }
        public OrderWithSpec(int orderId, string buyeremail) : base(O => (O.BuyerEmail == buyeremail && O.Id == orderId))
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);  
        }
    }
}
