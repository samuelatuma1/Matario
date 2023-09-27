using System;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Persistence.DataAccess.Common;
using Matario.Persistence.DbContexts;
using Matario.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Matario.Persistence.DataAccess.AuthenticationModule
{
	public class PermissionRepository : BaseRepository<Permission, long>, IPermissionRepository
	{
		public PermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

        public async Task<bool> IsUniquePermissionName(string permissionName)
        {
            var permission = await FirstOrDefaultAsync(r => r.Name == permissionName);
            return permission is null;
        }

        public async Task<Permission?> FindByIdAndUpdate(Permission model)
        {
            Permission? entity  = await FindByIdAsync(model.Id);
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
                 bool isUniquePermissionName = await IsUniquePermissionName(model.Name);
                if(!isUniquePermissionName)
                {
                    throw new DatabaseException("Permission name must be unique");
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
                throw new DatabaseException($"An error occured while updating Permission: {ex.Message}");
            }
            
        }

        public async Task<Permission?> GetPermissionByName(string name)
        {
            return await FirstOrDefaultAsync(permission => permission.Name.Equals(name));
        }

        public async Task<IEnumerable<Permission>> GetPermissionsForRoles(IEnumerable<long> roleIds)
        {
            return await _dbContext.Roles
                .Where(r => roleIds.Contains(r.Id))
                .SelectMany(r => r.Permissions)
                .Distinct()
                .ToListAsync();
        }
    }
}

