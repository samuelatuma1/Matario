using System;
namespace Matario.Models
{
	public class ErrorModel
	{
		public string Message { get; set; } = string.Empty;
		public IDictionary<string, string> Errors { get; set; }
		public ErrorModel()
		{
			Errors = new Dictionary<string, string>();
		}

        public ErrorModel(IDictionary<string, string> errors)
        {
            Errors = errors;
        }
    }
}

