using System;
using Matario.Domain.Enums.Common;

namespace Matario.Domain.Entities.Common
{
	public class BaseEntity<TId>
	{
		public TId Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public RecordStatus RecordStatus { get; set; }
    }
}

