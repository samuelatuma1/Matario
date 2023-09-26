using System;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Persistence.DataAccess.Common;
using Matario.Persistence.DbContexts;
using Matario.Persistence.Exceptions;

namespace Matario.Persistence.DataAccess.AuthenticationModule
{
	public class RoleRepository : BaseRepository<Role, long>, IRoleRepository
	{
		public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

        public async Task<bool> IsUniqueRoleName(string roleName)
        {
            var role = await FirstOrDefaultAsync(r => r.Name == roleName);
            return role is null;
        }

        public async Task<Role?> FindByIdAndUpdate(Role model)
        {
            Role? entity  = await FindByIdAsync(model.Id);
            if(entity is null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(model.Description))
            {
                entity.Description = model.Description;
            }
            bool isNameNotEmptyAndNewName = !string.IsNullOrEmpty(model.Name) && !entity.Name.Equals(model.Name, StringComparison.CurrentCultureIgnoreCase);
            if (isNameNotEmptyAndNewName)
            {
                // ensure name is unique before updating
                 bool isUniqueRoleName = await IsUniqueRoleName(model.Name);
                if(!isUniqueRoleName)
                {
                    throw new DatabaseException("Role name must be unique");
                }
                entity.Name = model.Name;
            }

            try
            {
                await UpdateAsync(entity);
                return entity;
            }
            catch(Exception ex)
            {
                throw new DatabaseException($"An error occured while updating Role: {ex.Message}");
            }
            
        }

    }
}

