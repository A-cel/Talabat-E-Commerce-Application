
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        //Task AddAsync(T t);
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
        Task<T?> GetWithSpec(ISpecification<T> spec);
        Task<int> CountWithSpecAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
