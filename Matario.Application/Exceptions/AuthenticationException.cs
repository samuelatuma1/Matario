using System;
namespace Matario.Application.Exceptions
{
	public class AuthenticationException : Exception
	{
        public IDictionary<string, string> Errors;
        public AuthenticationException(string message, IDictionary<string, string> errors) : base(message)
        {
            Errors = errors;
        }

        public AuthenticationException(Exception ex) : base(ex.Message)
        {
            Errors = new Dictionary<string, string>();
        }
        public AuthenticationException(string message) : base(message)
        {
            Errors = new Dictionary<string, string>();
        }


    }
}

