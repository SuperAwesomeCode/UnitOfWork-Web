using SuperAwesomeCode.Security;

namespace System
{
	/// <summary>Extensionn methods for bytes.</summary>
	public static class ByteExtensions
	{
		/// <summary>Encrypts the specified bytes.</summary>
		/// <param name="bytes">The bytes.</param>
		/// <param name="sharedSecretKey">The shared secret key.</param>
		/// <param name="salt">The salt string.</param>
		/// <returns></returns>
		public static byte[] Encrypt(this byte[] bytes, string sharedSecretKey, string salt)
		{
			if (bytes == null)
			{
				return null;
			}

			return Convert.FromBase64String(Crypto.EncryptStringAes(Convert.ToBase64String(bytes), sharedSecretKey, salt));
		}

		/// <summary>Decrypts the specified bytes.</summary>
		/// <param name="bytes">The bytes.</param>
		/// <param name="sharedSecretKey">The shared secret key.</param>
		/// <param name="salt">The salt string.</param>
		/// <returns></returns>
		public static byte[] Decrypt(this byte[] bytes, string sharedSecretKey, string salt)
		{
			if (bytes == null)
			{
				return null;
			}

			return Convert.FromBase64String(Crypto.DecryptStringAes(Convert.ToBase64String(bytes), sharedSecretKey, salt));
		}
	}
}
