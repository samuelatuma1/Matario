using System;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.UoW;
using Matario.Application.Exceptions;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class DeleteRoleRequestHandler : IRequestHandler<DeleteRoleRequest, Unit>
	{
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteRoleRequestHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
        {
            if(request.Id == 0)
            {
                throw new InvalidKeyException("Please use a valid Id for Role");
            }
            
            await _roleRepository.DeleteByIdAsync(request.Id, soft: false);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

