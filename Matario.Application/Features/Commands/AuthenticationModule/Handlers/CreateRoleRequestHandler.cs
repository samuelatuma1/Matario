using System;
using AutoMapper;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.UoW;
using Matario.Application.Exceptions;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Application.Features.Commands.AuthenticationModule.Validators;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class CreateRoleRequestHandler : IRequestHandler<CreateRoleRequest, Role>
	{
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleRequestHandler(IRoleRepository roleRepository, IAuthenticationRepository authenticationRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _authenticationRepository = authenticationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Role> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            // Validate request including verifying it is being created by a super admin
            var createRoleRequestValidator = new CreateRoleRequestValidator(_roleRepository, _authenticationRepository);
            var validationResult = await createRoleRequestValidator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
            {
                IDictionary<string, string> errors = validationResult.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage);

                throw new ValidationException("Some validation errors occured while creating role", errors);
            }


            // convert from CreateRoleRequest to Role
            Role role = _mapper.Map<Role>(request);

            await _roleRepository.AddAsync(role);
            await _unitOfWork.SaveChangesAsync();
            return role;
        }
    }
}

