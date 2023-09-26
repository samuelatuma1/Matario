using System;
using Matario.Domain.Entities.Common;

namespace Matario.Application.Contracts.DataAccess.Common
{
	public interface IBaseRepository<TEntity, TId>
		where TEntity : BaseEntity<TId>
	{
		public Task AddAsync(TEntity entity);

		public Task AddRangeAsync(TEntity entity);

		public Task UpdateAsync(TEntity entity);

        public Task UpdateRangeAsync(Func<TEntity, bool> query, TEntity entity);

		public Task<int> DeleteAsync(TEntity entity, bool soft = true);

		public Task<int> DeleteByIdAsync(TId id, bool soft = true);

		public Task<TEntity?> FindByIdAsync(TId id);

		public Task<IEnumerable<TEntity>> WhereAsync(Func<TEntity, bool> query);

		public Task<TEntity?> FirstOrDefaultAsync(Func<TEntity, bool> query);



    }
}

