using System;
using Matario.Application.Contracts.DataAccess.Common;
using Matario.Application.Utilities;
using Matario.Domain.Entities.Common;
using Matario.Persistence.DbContexts;

namespace Matario.Persistence.DataAccess.Common
{
	public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
		where TEntity : BaseEntity<TId>
	{
        protected readonly ApplicationDbContext _dbContext;
		public BaseRepository(ApplicationDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedAt = DateAndTimeUtilities.Now();
            entity.UpdatedAt = DateAndTimeUtilities.Now();
            entity.RecordStatus = Domain.Enums.Common.RecordStatus.Active;

            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task AddRangeAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(TEntity entity, bool soft = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(TId id, bool soft = true)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> WhereAsync(Func<TEntity, bool> query)
        {
            await Task.CompletedTask;
            return _dbContext.Set<TEntity>().Where(query);
        }

        public Task<TEntity?> FindByIdAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Func<TEntity, bool> query)
        {
            await Task.CompletedTask;
            return _dbContext.Set<TEntity>().FirstOrDefault(query);
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(Func<TEntity, bool> query, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

