using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Core.UnitOfWork.Contract
{
    public interface IUnitOfWork : IAsyncDisposable 
    {
        //public IGenericRepository<Product> productrepository  { get; set; }
        //public IGenericRepository<Order> orderrepository  { get; set; }
        //public IGenericRepository<OrderItem> orderitemrepository  { get; set; }
        //public IGenericRepository<ProductBrand> productbrandrepository  { get; set; }
        //public IGenericRepository<ProductCategory> productcategoryrepository  { get; set; }
        //public IGenericRepository<DeliveryMethod> deliverymethodrepository { get; set; }
        IGenericRepository<T> Repositories<T>()where T :BaseEntity;
        Task<int> CompleteAsync();
    }
}
