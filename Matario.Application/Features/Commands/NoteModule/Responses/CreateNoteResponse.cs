using System;
namespace Matario.Application.Features.Commands.NoteModule.Responses
{
	public class CreateNoteResponse
	{
        public long Id { get; set; }

        public required string Title { get; set; }

        public required string Content { get; set; }

        public bool Pinned { get; set; } = false;

        public DateTime TimeStamp { get; set; }

    }
}

