using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.OrderSpecs;
using Talabat.Core.UnitOfWork.Contract;
using Talabat.Repository.Data;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICartRepository cartrepository;

        //private readonly StoreContext storeContext;
        //private readonly IGenericRepository<Order> orderrepo;
        //private readonly IGenericRepository<Product> genericRepository;
        //private readonly IGenericRepository<DeliveryMethod> deliveryrepo;

        public OrderService(IUnitOfWork unitOfWork , ICartRepository cartRepository/*StoreContext storeContext,IGenericRepository<Order> orderrepo , ICartRepository cartRepository , IGenericRepository<Product> genericRepository , IGenericRepository<DeliveryMethod> deliveryrepo*/)
        {
            this.unitOfWork = unitOfWork;
            cartrepository = cartRepository;
            //this.storeContext = storeContext;
            //this.orderrepo = orderrepo;
            //this.genericRepository = genericRepository;
            //this.deliveryrepo = deliveryrepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliverymethodId, Address address)
        {
            var basket = await cartrepository.GetCartAsync(basketId);
            
             var orderitems = new List<OrderItem>();
            if (basket?.Items?.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await unitOfWork.Repositories<Product>().GetAsync(item.Id);
                    var prodsitem = new OrderProductsItem(item.Id , product.Name , product.PictureUrl);
                    var orderitem = new OrderItem(prodsitem, product.Price, item.Quantity);
                    orderitems.Add(orderitem);
                }
                
            }
            var subtotal = orderitems.Sum(p=>p.Price * p.Quantity);
            var deliverymethod = await unitOfWork.Repositories<DeliveryMethod>().GetAsync(deliverymethodId);
            var order = new Order(buyerEmail , address ,deliverymethod , orderitems , subtotal);
            await unitOfWork.Repositories<Order>().AddAsync(order);
           var result =  await unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;
        }

        public async Task<Order?> GetOrderByIdForUserAsync(int orderid, string buyerEmail)
        {
            var repo =  unitOfWork.Repositories<Order>();
            var spec = new OrderWithSpec(orderid, buyerEmail);
            var order = await repo.GetWithSpec(spec);
            return order; 
        } 

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderrepo = unitOfWork.Repositories<Order>();
            var orderspec = new OrderWithSpec(buyerEmail);
            var getquery = await orderrepo.GetAllWithSpec(orderspec);
            return getquery;
        }
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var repo = unitOfWork.Repositories<DeliveryMethod>();
            var Dmethods = await repo.GetAllAsync();
            return Dmethods;
        }
    }
}
