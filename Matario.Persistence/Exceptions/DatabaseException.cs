using System;
namespace Matario.Persistence.Exceptions
{
	public class DatabaseException : Exception
	{
        public IDictionary<string, string> Errors;
        public DatabaseException(string message, IDictionary<string, string> errors) : base(message)
        {
            Errors = errors;
        }

        public DatabaseException(string message) : base(message)
        {
            Errors = new Dictionary<string, string>();
        }
    }
}

