using System;
namespace Matario.Config
{
	public class RateLimitOptions
	{
		public int PermitLimit { get; set; }

		public int Window { get; set; }

		public int QueueLimit { get; set; }
        public RateLimitOptions()
		{
		}
	}
}

