﻿using System;
using Matario.Domain.Entities.Common;

namespace Matario.Domain.Entities.AuthenticationModule
{
	public class Permission : BaseEntity<long>
	{
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public long? CreatedBy { get; set; }

		public List<Role> Roles { get; set; } = new();
    }
}

