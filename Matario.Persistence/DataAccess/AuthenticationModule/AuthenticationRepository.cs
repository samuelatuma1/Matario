using System;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Persistence.DataAccess.Common;
using Matario.Persistence.DbContexts;

namespace Matario.Persistence.DataAccess.AuthenticationModule;

public class AuthenticationRepository : BaseRepository<User, long>, IAuthenticationRepository 
 {

	public AuthenticationRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
	}

    public async Task<bool> IsUniqueEmail(string email)
    {
        var response = await FirstOrDefaultAsync(user => user.Email.Equals(email));

        return response is not null;
    }
}

