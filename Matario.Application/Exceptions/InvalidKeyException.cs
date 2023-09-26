using System;
namespace Matario.Application.Exceptions
{
	public class InvalidKeyException : Exception
	{
        public IDictionary<string, string> Errors;
        public InvalidKeyException(string message, IDictionary<string, string> errors) : base(message)
        {
            Errors = errors;
        }

        public InvalidKeyException(Exception ex) : base(ex.Message)
        {
            Errors = new Dictionary<string, string>();
        }
        public InvalidKeyException(string message) : base(message)
        {
            Errors = new Dictionary<string, string>();
        }
    }
}

