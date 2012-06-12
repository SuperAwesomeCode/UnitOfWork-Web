using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SuperAwesomeCode.Security
{
	/// <summary>Class used to Encrypt and Decrypt strings.</summary>
	public class Crypto
	{
		/// <summary>
		/// Encrypt the given string using AES.  The string can be decrypted using
		/// DecryptStringAes().  The sharedSecret parameters must match.
		/// </summary>
		/// <param name="plainText">The text to encrypt.</param>
		/// <param name="sharedSecret">A password used to generate a key for encryption.</param>
		/// <param name="salt">The salt used for encryption.</param>
		/// <returns></returns>
		public static string EncryptStringAes(string plainText, string sharedSecret, string salt)
		{
			if (string.IsNullOrEmpty(plainText))
			{
				throw new ArgumentNullException("plainText");
			}

			if (string.IsNullOrEmpty(sharedSecret))
			{
				throw new ArgumentNullException("sharedSecret");
			}

			string returnValue = null;
			AesManaged aesManaged = null;

			try
			{
				// generate the key from the shared secret and the salt
				Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, Encoding.ASCII.GetBytes(salt));

				// Create a RijndaelManaged object
				// with the specified key and IV.
				aesManaged = new AesManaged();
				aesManaged.Key = key.GetBytes(aesManaged.KeySize / 8);
				aesManaged.IV = key.GetBytes(aesManaged.BlockSize / 8);

				// Create a decrytor to perform the stream transform.
				ICryptoTransform encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);

				// Create the streams used for encryption.
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
						{
							//Write all data to the stream.
							streamWriter.Write(plainText);
						}
					}

					returnValue = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			finally
			{
				// Clear the RijndaelManaged object.
				if (aesManaged != null)
				{
					aesManaged.Clear();
				}
			}

			// Return the encrypted bytes from the memory stream.
			return returnValue;
		}

		/// <summary>
		/// Decrypt the given string.  Assumes the string was encrypted using
		/// EncryptStringAes(), using an identical sharedSecret.
		/// </summary>
		/// <param name="cipherText">The text to decrypt.</param>
		/// <param name="sharedSecret">A password used to generate a key for decryption.</param>
		/// <param name="salt">The salt used for encryption.</param>
		/// <returns></returns>
		public static string DecryptStringAes(string cipherText, string sharedSecret, string salt)
		{
			if (string.IsNullOrEmpty(cipherText))
			{
				throw new ArgumentNullException("cipherText");
			}

			if (string.IsNullOrEmpty(sharedSecret))
			{
				throw new ArgumentNullException("sharedSecret");
			}

			// Declare the AesManaged object
			// used to decrypt the data.
			AesManaged aesManaged = null;

			// Declare the string used to hold
			// the decrypted text.
			string plaintext = null;

			try
			{
				// generate the key from the shared secret and the salt
				Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, Encoding.ASCII.GetBytes(salt));

				// Create a RijndaelManaged object
				// with the specified key and IV.
				aesManaged = new AesManaged();
				aesManaged.Key = key.GetBytes(aesManaged.KeySize / 8);
				aesManaged.IV = key.GetBytes(aesManaged.BlockSize / 8);

				// Create a decrytor to perform the stream transform.
				ICryptoTransform decryptor = aesManaged.CreateDecryptor(aesManaged.Key, aesManaged.IV);

				// Create the streams used for decryption.
				byte[] bytes = Convert.FromBase64String(cipherText);
				using (MemoryStream memoryStream = new MemoryStream(bytes))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader(cryptoStream))
						{
							// Read the decrypted bytes from the decrypting stream
							// and place them in a string.
							plaintext = streamReader.ReadToEnd();
						}
					}
				}
			}
			catch
			{
				return string.Empty;
			}
			finally
			{
				// Clear the RijndaelManaged object.
				if (aesManaged != null)
				{
					aesManaged.Clear();
				}
			}

			return plaintext;
		}
	}
}