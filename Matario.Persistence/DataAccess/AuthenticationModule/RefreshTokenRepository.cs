using System;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Persistence.DataAccess.Common;
using Matario.Persistence.DbContexts;

namespace Matario.Persistence.DataAccess.AuthenticationModule
{
	public class RefreshTokenRepository : BaseRepository<RefreshToken, long>, IRefreshTokenRepository
    {
		public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
		}
	}
}

