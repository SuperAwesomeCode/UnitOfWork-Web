using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SuperAwesomeCode.Security
{
	/// <summary>Class used to Encrypt and Decrypt strings.</summary>
	public class Crypto
	{
		/// <summary>Encrypt the given string using AES.  The string can be decrypted using
		/// DecryptStringAes().  The sharedSecret parameters must match.</summary>
		/// <param name="plainText">The text to encrypt.</param>
		/// <param name="sharedSecret">A password used to generate a key for encryption.</param>
		/// <param name="salt">The salt used for encryption.</param>
		/// <returns></returns>
		public static string EncryptStringAes(string plainText, string sharedSecret, string salt)
		{
			Guard.AgainstNullOrEmpty(plainText);
			Guard.AgainstNullOrEmpty(sharedSecret);
			Guard.AgainstNullOrEmpty(salt);

			try
			{
				using (var key = new Rfc2898DeriveBytes(sharedSecret, Encoding.ASCII.GetBytes(salt)))
				using (var aesManaged = new AesManaged())
				{
					aesManaged.Key = key.GetBytes(aesManaged.KeySize / 8);
					aesManaged.IV = key.GetBytes(aesManaged.BlockSize / 8);

					using (var encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV))
					using (var memoryStream = new MemoryStream())
					using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
					using (var streamWriter = new StreamWriter(cryptoStream))
					{
						//Write all data to the stream.
						streamWriter.Write(plainText);
						streamWriter.Close();
						return Convert.ToBase64String(memoryStream.ToArray());
					}
				}
			}
			catch
			{
				return string.Empty;
			}
		}

		/// <summary>Decrypt the given string.  Assumes the string was encrypted using
		/// EncryptStringAes(), using an identical sharedSecret.</summary>
		/// <param name="cipherText">The text to decrypt.</param>
		/// <param name="sharedSecret">A password used to generate a key for decryption.</param>
		/// <param name="salt">The salt used for encryption.</param>
		/// <returns></returns>
		public static string DecryptStringAes(string cipherText, string sharedSecret, string salt)
		{
			Guard.AgainstNullOrEmpty(cipherText);
			Guard.AgainstNullOrEmpty(sharedSecret);
			Guard.AgainstNullOrEmpty(salt);

			try
			{
				byte[] bytes = Convert.FromBase64String(cipherText);
				using (var key = new Rfc2898DeriveBytes(sharedSecret, Encoding.ASCII.GetBytes(salt)))
				using (var aesManaged = new AesManaged())
				{
					aesManaged.Key = key.GetBytes(aesManaged.KeySize / 8);
					aesManaged.IV = key.GetBytes(aesManaged.BlockSize / 8);

					using (var decryptor = aesManaged.CreateDecryptor(aesManaged.Key, aesManaged.IV))
					using (var memoryStream = new MemoryStream(bytes))
					using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
					using (var streamReader = new StreamReader(cryptoStream))
					{
						// Read the decrypted bytes from the decrypting stream
						// and place them in a string.
						return streamReader.ReadToEnd();
					}
				}
			}
			catch
			{
				return string.Empty;
			}
		}
	}
}