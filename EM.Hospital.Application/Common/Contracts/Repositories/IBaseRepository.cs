using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EM.Hospital.Application.Common.Contracts.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity?> FindByPredicateAsync(Expression<Func<TEntity, bool>> predicate);
        Task<(ICollection<TResult> Result, int TotalCount)> ListAsync<TResult, TKey>(
            Func<TEntity, TResult> selector,
            Func<TEntity, bool> predicate,
            Func<TEntity, TKey> orderBy,
            int page = 1, int pageSize = 10);
    }
}
