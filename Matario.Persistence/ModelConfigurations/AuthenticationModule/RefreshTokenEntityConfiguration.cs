using System;
using Matario.Domain.Entities.AuthenticationModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matario.Persistence.ModelConfigurations.AuthenticationModule
{
	public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
	{
		public RefreshTokenEntityConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne<User>();
        }
    }
}

