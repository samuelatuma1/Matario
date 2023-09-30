using System;
using System.Data;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Domain.Enums.AuthenticationModule;
using Matario.Persistence.DataAccess.Common;
using Matario.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Matario.Persistence.DataAccess.AuthenticationModule;

public class AuthenticationRepository : BaseRepository<User, long>, IAuthenticationRepository 
 {

	public AuthenticationRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
	}

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _dbContext.Users.Include(u => u.Roles).Include(u => u.Organisation).FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    public async Task<bool> IsSuperAdmin(long userId)
    {
        User? user = await FirstOrDefaultAsync(user => user.Id == userId);
        if(user is null)
        {
            return false;
        }
        return user.UserRole == UserRole.SuperAdmin;
    }

    public async Task<bool> IsUniqueEmail(string email)
    {
        var response = await FirstOrDefaultAsync(user => user.Email.Equals(email));

        return response is null;
    }

}

