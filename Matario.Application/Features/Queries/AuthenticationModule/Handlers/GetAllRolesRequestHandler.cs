using System;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Features.Queries.AuthenticationModule.Requests;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Domain.Enums.Common;
using MediatR;

namespace Matario.Application.Features.Queries.AuthenticationModule.Handlers
{
	public class GetAllRolesRequestHandler : IRequestHandler<GetAllRolesRequest, IEnumerable<Role>>
    {
        private readonly IRoleRepository _roleRepository;
        public GetAllRolesRequestHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> Handle(GetAllRolesRequest request, CancellationToken cancellationToken)
        {
            return await _roleRepository.WhereAsync(role => role.RecordStatus != RecordStatus.Deleted);
        }
    }
}

