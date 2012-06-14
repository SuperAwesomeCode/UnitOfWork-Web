using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests.Extensions
{
	[TestClass()]
	public class ByteExtensionsTest : BaseUnitTest
	{
		/// <summary>A test for Encrypt</summary>
		[TestMethod()]
		public void EncryptDecryptTest()
		{
			byte[] bytes = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
			string sharedSecretKey = "ShhSecret";
			string salt = "IPreferIodine";

			var encrypted = bytes.Encrypt(sharedSecretKey, salt);
			var decrypted = encrypted.Decrypt(sharedSecretKey, salt);

			Assert.IsTrue(encrypted.Length > 0, "Encryption returned empty.");
			Assert.IsFalse(this.ByteArraysHaveEqualValues(bytes, encrypted), "Encryption failed");
			Assert.IsTrue(this.ByteArraysHaveEqualValues(bytes, decrypted), "Decryption failed");
		}

		private bool ByteArraysHaveEqualValues(byte[] left, byte[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}

			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}

			return true;
		}
	}
}
