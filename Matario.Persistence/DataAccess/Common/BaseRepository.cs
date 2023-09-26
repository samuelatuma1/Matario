using System;
using Matario.Application.Contracts.DataAccess.Common;
using Matario.Application.Utilities;
using Matario.Domain.Entities.Common;
using Matario.Domain.Enums.Common;
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

        public virtual async Task AddAsync(TEntity entity)
        {
            entity.CreatedAt = DateAndTimeUtilities.Now();
            entity.UpdatedAt = DateAndTimeUtilities.Now();
            entity.RecordStatus = Domain.Enums.Common.RecordStatus.Active;

            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public Task AddRangeAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(TEntity entity, bool soft = true)
        {
            return await DeleteByIdAsync(entity.Id, soft);
        }

        public async Task<int> DeleteByIdAsync(TId id, bool soft = true)
        {
            TEntity? entityExists = await FindByIdAsync(id);
            int count = 0;
            if (entityExists is not null)
            {

                if (soft)
                {
                    entityExists.RecordStatus = RecordStatus.Deleted;
                }
                else
                {
                    _dbContext.Set<TEntity>().Remove(entityExists);
                }
                count = 1;
            }
            return count;
        }

        public async Task<IEnumerable<TEntity>> WhereAsync(Func<TEntity, bool> query)
        {
            await Task.CompletedTask;
            return _dbContext.Set<TEntity>().Where(query);
        }

        public async Task<TEntity?> FindByIdAsync(TId id)
        {
            await Task.CompletedTask;
            return _dbContext.Set<TEntity>().FirstOrDefault(entity => entity.Id.Equals(id));
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Func<TEntity, bool> query)
        {
            await Task.CompletedTask;
            return _dbContext.Set<TEntity>().FirstOrDefault(query);
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {
            await Task.CompletedTask;
            _dbContext.Set<TEntity>().Update(entity);
        }

        public Task UpdateRangeAsync(Func<TEntity, bool> query, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

