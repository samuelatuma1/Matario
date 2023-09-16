using System;
using Matario.Domain.Entities.AuthenticationModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matario.Persistence.ModelConfigurations.AuthenticationModule
{
	public class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
		public UserEntityConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(256);
        }
    }
}

