using System;
using AutoMapper;
using Matario.Application.Features.Commands.NoteModule.Requests;
using Matario.Application.Features.Commands.NoteModule.Responses;
using Matario.Domain.Entities.NoteModule;

namespace Matario.Application.AutoMapperProfiles
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<CreateNoteRequest, Note>().ReverseMap();
			CreateMap<Note, CreateNoteResponse>().ReverseMap();
		}
	}
}

