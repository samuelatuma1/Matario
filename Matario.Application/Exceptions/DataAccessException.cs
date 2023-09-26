using System;
namespace Matario.Application.Exceptions
{
	public class DataAccessException : Exception
	{
        public IDictionary<string, string> Errors;
        public DataAccessException(string message, IDictionary<string, string> errors) : base(message)
        {
            Errors = errors;
        }

        public DataAccessException(Exception ex) : base(ex.Message)
        {
            Errors = new Dictionary<string, string>();
        }
        public DataAccessException(string message) : base(message)
        {
            Errors = new Dictionary<string, string>();
        }
    }
}

