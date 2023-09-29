using System;
using Matario.Application.Contracts.DataAccess.Common;
using Matario.Domain.Entities.OrganisationModule;

namespace Matario.Application.Contracts.DataAccess.OrganisationModule
{
	public interface IOrganisationRepository : IBaseRepository<Organisation, long>
	{
		Task<bool> IsUniqueName(string name);

		Task<bool> OrganisationWithNameExists(string name);

		Task<Organisation?> GetOrganisationByName(string name);

    }
}

