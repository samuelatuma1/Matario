using System;
using Matario.Domain.Entities.OrganisationModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matario.Persistence.ModelConfigurations.OrganisationModule
{
	public class OrganisationEntityConfiguration : IEntityTypeConfiguration<Organisation>
	{
		public OrganisationEntityConfiguration()
		{

		}

        public void Configure(EntityTypeBuilder<Organisation> builder)
        {
            builder.HasKey(organisation => organisation.Id);
            builder.HasIndex(organisation => organisation.Name);
            builder.HasMany(organisation => organisation.Users)
                .WithOne()
                .HasForeignKey(user => user.OrganisationId)
                .IsRequired(false);
        }
    }
}

