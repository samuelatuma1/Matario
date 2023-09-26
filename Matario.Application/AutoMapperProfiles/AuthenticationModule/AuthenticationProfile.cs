using System;
using AutoMapper;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.AutoMapperProfiles.AuthenticationModule
{
	public class AuthenticationProfile : Profile
	{
		public AuthenticationProfile()
		{
			CreateMap<SignupRequest, User>().ReverseMap();
			CreateMap<CreateRoleRequest, Role>();
			CreateMap<CreatePermissionRequest, Permission>();
		}
	}
}

