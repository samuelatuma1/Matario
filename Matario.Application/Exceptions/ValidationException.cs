using System;
namespace Matario.Application.Exceptions
{
	public class ValidationException : Exception
	{
		public IDictionary<string, string> Errors;
		public ValidationException(string message, IDictionary<string, string> errors) : base(message)
		{
			Errors = errors;
		}

		public ValidationException(string message) : base(message)
		{
			Errors = new Dictionary<string, string>();
		}
    }
}

