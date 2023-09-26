using System;
using Matario.Application.Utilities;
using Matario.Domain.Entities.AuthenticationModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matario.Persistence.ModelConfigurations.AuthenticationModule
{
	public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
	{
		public PermissionEntityConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(permission => permission.Id);
            builder.HasMany(permission => permission.Roles)
                .WithMany(role => role.Permissions)
                .UsingEntity<RolesPermissions>(); // modelling many to many relationship between Permission and User

            // add seed data isSuperAdmin to table
            builder.HasData(
                new Permission()
                {
                    Id = 1,
                    Name = "Create Manager",
                    RecordStatus = Domain.Enums.Common.RecordStatus.Active,
                    CreatedAt = DateAndTimeUtilities.Now(),
                    UpdatedAt = DateAndTimeUtilities.Now(),
                    Description = "Allows users with permission to create Managers"
                    
                }
                ) ;
        }
    }
}

