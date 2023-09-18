using System;
namespace Matario.Application.Exceptions
{
	public class UnAuthorizedException : Exception
	{
        public IDictionary<string, string> Errors;
        public UnAuthorizedException(string message, IDictionary<string, string> errors) : base(message)
        {
            Errors = errors;
        }

        public UnAuthorizedException(string message) : base(message)
        {
            Errors = new Dictionary<string, string>();
        }
    }
}

