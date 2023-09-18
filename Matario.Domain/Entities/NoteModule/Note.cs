using System;
using Matario.Domain.Entities.Common;

namespace Matario.Domain.Entities.NoteModule
{
	public class Note : BaseEntity<long>
	{
        // { id: 1, title: 'Note 1 title', content: 'Note 1 Content', timestamp: Date.now(), pinned: true }
		public long UserId { get; set;  }

        public required string Title { get; set; }

		public required string Content { get; set; }

		public DateTime TimeStamp { get; set; }

		public bool Pinned { get; set; }
	}
}

