using System;
using AutoMapper;
using Matario.Application.Features.Commands.OrganisationModule.Commands;
using Matario.Domain.Entities.OrganisationModule;

namespace Matario.Application.AutoMapperProfiles.OrganisationModule
{
	public class OrganisationProfile : Profile
	{
		public OrganisationProfile()
		{
			CreateMap<CreateOrganisationCommand, Organisation>();
		}
	}
}

