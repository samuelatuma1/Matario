using System;
using Matario.Application.Features.Commands.NoteModule.Responses;
using MediatR;

namespace Matario.Application.Features.Commands.NoteModule.Requests
{
	public class CreateNoteRequest : IRequest<CreateNoteResponse>
	{
        public long UserId { get; set; }

        public required string Title { get; set; }

        public required string Content { get; set; }

        public bool Pinned { get; set; } = false;
    }
}

