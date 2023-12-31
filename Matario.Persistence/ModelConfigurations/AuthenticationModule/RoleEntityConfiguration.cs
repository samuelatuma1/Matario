﻿using System;
using Matario.Application.Utilities;
using Matario.Domain.Entities.AuthenticationModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matario.Persistence.ModelConfigurations.AuthenticationModule
{
	public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
	{
		public RoleEntityConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasMany(e => e.Users)
                .WithMany(u => u.Roles)
                .UsingEntity<UsersRoles>(); // modelling many to many relationship between Role and User

            // add seed data isSuperAdmin to table
            builder.HasData(
                new Role()
                {
                    Id = 1,
                    Name = "SuperAdmin",
                    RecordStatus = Domain.Enums.Common.RecordStatus.Active,
                    CreatedAt = DateAndTimeUtilities.Now(),
                    UpdatedAt = DateAndTimeUtilities.Now(),
                    Description = "Super Admin Priviledges"
                },
                new Role()
                {
                    Id = 2,
                    Name = "Admin",
                    RecordStatus = Domain.Enums.Common.RecordStatus.Active,
                    CreatedAt = DateAndTimeUtilities.Now(),
                    UpdatedAt = DateAndTimeUtilities.Now(),
                    Description = "Admin Priviledges"
                },
                new Role()
                {
                    Id = 3,
                    Name = "CorporateAdmin",
                    RecordStatus = Domain.Enums.Common.RecordStatus.Active,
                    CreatedAt = DateAndTimeUtilities.Now(),
                    UpdatedAt = DateAndTimeUtilities.Now(),
                    Description = "CorporateAdmin Priviledges"
                },
                new Role()
                {
                    Id = 4,
                    Name = "Manager",
                    RecordStatus = Domain.Enums.Common.RecordStatus.Active,
                    CreatedAt = DateAndTimeUtilities.Now(),
                    UpdatedAt = DateAndTimeUtilities.Now(),
                    Description = "Manager Priviledges"
                }
                ) ;
        }
    }
}

