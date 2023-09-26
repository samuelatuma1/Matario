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
	public class CreatePermissionRequestHandler : IRequestHandler<CreatePermissionRequest, Permission>
	{
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePermissionRequestHandler(IPermissionRepository permissionRepository, IAuthenticationRepository authenticationRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _authenticationRepository = authenticationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Permission> Handle(CreatePermissionRequest request, CancellationToken cancellationToken)
        {
            // Validate request including verifying it is being created by a super admin
            var createPermissionRequestValidator = new CreatePermissionRequestValidator(_permissionRepository, _authenticationRepository);
            var validationResult = await createPermissionRequestValidator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
            {
                IDictionary<string, string> errors = validationResult.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage);

                throw new ValidationException("Some validation errors occured while creating Permission", errors);
            }


            // convert from CreatePermissionRequest to Permission
            Permission permission = _mapper.Map<Permission>(request);

            await _permissionRepository.AddAsync(permission);
            await _unitOfWork.SaveChangesAsync();
            return permission;
        }
    }
}

