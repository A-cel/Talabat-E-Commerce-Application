using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByAsc { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 0; 
        public bool PaginationEnable { get; set ; }

        public BaseSpecification()
        {    
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria )
        {
            Criteria = criteria;
        }
        public void ApplyPagination(int skip , int take)
        {
            PaginationEnable = true;
            Skip = skip;
            Take = take;
        }
        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderByAsc = orderBy;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderBy) => OrderByDesc = orderBy;
        
    }
}
