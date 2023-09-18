using System;
using AutoMapper;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Exceptions;
using Matario.Application.Features.Commands.NoteModule.Requests;
using Matario.Application.Features.Commands.NoteModule.Responses;
using Matario.Application.Utilities;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Domain.Entities.NoteModule;
using MediatR;

namespace Matario.Application.Features.Commands.NoteModule.Handlers
{
	public class CreateNoteRequestHandler : IRequestHandler<CreateNoteRequest, CreateNoteResponse>
	{
        private readonly IAuthenticationRepository _authenticatinRepository;
        private readonly IMapper _mapper;
        public CreateNoteRequestHandler(IAuthenticationRepository authenticatinRepository, IMapper mapper)
        {
            _authenticatinRepository = authenticatinRepository;
            _mapper = mapper;
        }

        public async Task<CreateNoteResponse> Handle(CreateNoteRequest request, CancellationToken cancellationToken)
        {
            // get user with matching user id
            User user = await _authenticatinRepository.FindByIdAsync(request.UserId) ?? throw new UnAuthorizedException("Unauthorized");
            // if user does not exists throw error

            // convert request to Note
            Note note = _mapper.Map<Note>(request);
            note.TimeStamp = DateAndTimeUtilities.Now();
            // save request

            // return NoteResponse
            throw new NotImplementedException();
        }
    }
}

