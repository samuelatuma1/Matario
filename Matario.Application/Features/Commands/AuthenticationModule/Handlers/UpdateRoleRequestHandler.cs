using System;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Exceptions;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class UpdateRoleRequestHandler : IRequestHandler<UpdateRoleRequest, Role?>
	{
        private readonly IRoleRepository _roleRepository;
        public UpdateRoleRequestHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role?> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var roleUpdate = new Role()
            {
                Id = request.Id,
                Name = request.Name ?? string.Empty,
                Description = request.Description ?? string.Empty
            };
            try
            {
                Role? role = await _roleRepository.FindByIdAndUpdate(roleUpdate);
                return role;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }
    }
}

