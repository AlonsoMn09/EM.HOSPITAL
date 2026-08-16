using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Domain.Common;
using EM.Hospital.Infraestructure.Configuration.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EM.Hospital.Infraestructure.Adapters.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly HospitalDbContext _context;
        public BaseRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id && x.Active);
        }

        public async Task<TEntity?> FindByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<(ICollection<TResult> Result, int TotalCount)> ListAsync<TResult, TKey>(
            Func<TEntity, TResult> selector,
            Func<TEntity, bool> predicate,
            Func<TEntity, TKey> orderBy,
            int page = 1, int pageSize = 10)
        {
            var result = _context.Set<TEntity>()
                .Where(predicate)
                .OrderByDescending(orderBy)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(selector)
                .ToList();

            var totalCount = _context.Set<TEntity>().Count(predicate);

            return (result, totalCount);
        }
    }
}
