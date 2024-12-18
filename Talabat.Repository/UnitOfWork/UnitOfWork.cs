using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.UnitOfWork.Contract;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;

namespace Talabat.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable repository { get; set; }

        public UnitOfWork(StoreContext storeContext)
        {
          _storeContext = storeContext;
            repository = new Hashtable();
        }
        /*Comment it Because it cintains nulls*/
        //public IGenericRepository<Product> productrepository { get ; set ; }
        //public IGenericRepository<Order> orderrepository { get ; set ; }
        //public IGenericRepository<DeliveryMethod> deliverymethodrepository { get ; set; }
        //public IGenericRepository<OrderItem> orderitemrepository { get; set; }
        //public IGenericRepository<ProductBrand> productbrandrepository { get ; set; }
        //public IGenericRepository<ProductCategory> productcategoryrepository { get; set; }

        public IGenericRepository<T> Repositories<T>() where T : BaseEntity
        {
            var repository = new Hashtable();
            var key = typeof(T).Name;

            if (!repository.ContainsKey(typeof(T))) { 
                var repo = new GenericRepository<T>(_storeContext);
            repository.Add(key,repo);

            }
            return repository[key] as IGenericRepository<T>;

        }
        public async Task<int> CompleteAsync()
        => await _storeContext.SaveChangesAsync();
        

        public async ValueTask DisposeAsync()
        => await _storeContext.DisposeAsync();

    }
}
