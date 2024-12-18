using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;
        public GenericRepository(StoreContext context)
        {
            _storeContext = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
      
            return  await _storeContext.Set<T>().ToListAsync(); 
        }

        public async Task<T?> GetAsync(int id)
        {
         
            return await _storeContext.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpec( ISpecification<T> spec)
        {
            
            return await QueryWithSpec(spec).ToListAsync(); 

        }
        public async Task<T?> GetWithSpec( ISpecification<T> spec)
        {

            return await QueryWithSpec(spec).FirstOrDefaultAsync();

        }
        public async Task<int> CountWithSpecAsync(ISpecification<T> spec)
        {

            return await QueryWithSpec(spec).CountAsync();

        }

        private IQueryable<T> QueryWithSpec(ISpecification<T> spec)
        {

            return  SpecificationBuilder<T>.Query(_storeContext.Set<T>(), spec);

        }

        public async Task AddAsync(T entity)
        => await _storeContext.AddAsync(entity);

        public void Update(T entity)
        {
            _storeContext.Update(entity);
        }

        public void Delete(T entity)
        {
            _storeContext.Remove(entity);
        }
    }
}
