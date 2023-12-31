﻿using System;
using System.Security.Claims;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Contracts.Services.AuthenticationServiceModule
{
	public interface IManageJwtService 
	{
        Task<AccessTokenDTO> GenerateAccessTokenAsync(User user);

        Task<RefreshToken> GenerateRefreshTokenForUserAsync(User user);

        Task<AuthenticationResponse> GenerateAccessAndRefreshToken(User user);

        Task<IEnumerable<Claim>> DecryptToken(string token);

        Task<bool> IsSuperAdmin(string token);

        Task<bool> UserHasPermission(string token, string permissons);

        Task<HasPermissionAndOrganisationNameDTO> ValidatePermissionAndGetOrganisationName(string token, string permissions);
    }
}

