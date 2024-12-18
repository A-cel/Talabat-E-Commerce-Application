using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories
{
    public static class SpecificationBuilder<T> where T : BaseEntity
    {

        public static IQueryable<T> Query(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;
                
                if(spec.Criteria is not null)
                query =query.Where(spec.Criteria);

            if (spec.OrderByAsc is not null)
                query = query.OrderBy(spec.OrderByAsc);
            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            if (spec.PaginationEnable)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (current, input) => current.Include(input) );
            return query;
        }
    }
}
