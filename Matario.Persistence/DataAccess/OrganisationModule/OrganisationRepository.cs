﻿using System;
using Matario.Application.Contracts.DataAccess.OrganisationModule;
using Matario.Domain.Entities.OrganisationModule;
using Matario.Persistence.DataAccess.Common;
using Matario.Persistence.DbContexts;

namespace Matario.Persistence.DataAccess.OrganisationModule
{
	public class OrganisationRepository : BaseRepository<Organisation, long>, IOrganisationRepository
	{
        
		public OrganisationRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

        public async Task<bool> IsUniqueName(string name)
        {
            await Task.CompletedTask;
            Organisation? organisation = _dbContext.Organisations.FirstOrDefault(org => org.Name.Equals(name));

            return organisation is null;

        }
    }
}

