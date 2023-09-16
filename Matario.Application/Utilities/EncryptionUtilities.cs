using System;
using System.Security.Cryptography;
using System.Text;

namespace Matario.Application.Utilities
{
	public static class EncryptionUtilities
	{
		public static string HashString(string str, string key)
		{
			byte[] strBytes = Encoding.UTF8.GetBytes(str);
			byte[] keyBytes = Encoding.UTF8.GetBytes(key);

			using var hmac = new HMACSHA512(keyBytes);
			byte[] hashedStringByte = hmac.ComputeHash(strBytes);
			return BitConverter.ToString(hashedStringByte).Replace("-", "");
			
		}
	}
}

