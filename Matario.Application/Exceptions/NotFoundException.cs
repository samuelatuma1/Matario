using System;
namespace Matario.Application.Exceptions
{
	public class NotFoundException : Exception
	{
        public IDictionary<string, string> Errors;
        public NotFoundException(string message, IDictionary<string, string> errors) : base(message)
        {
            Errors = errors;
        }

        public NotFoundException(Exception ex) : base(ex.Message)
        {
            Errors = new Dictionary<string, string>();
        }
        public NotFoundException(string message) : base(message)
        {
            Errors = new Dictionary<string, string>();
        }
    }
}

