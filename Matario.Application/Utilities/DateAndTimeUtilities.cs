using System;
namespace Matario.Application.Utilities
{
	public static class DateAndTimeUtilities
	{
		public static DateTime Now() => DateTime.UtcNow;

		public static DateTime AddMinutes(int minutes) => Now().AddMinutes(minutes);
	}

	
}

