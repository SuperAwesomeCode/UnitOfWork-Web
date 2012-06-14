using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperAwesomeCode.Security;

namespace SuperAwesomeCode.UnitTests.Security
{
	[TestClass()]
	public class CryptoTest : BaseUnitTest
	{
		[TestMethod()]
		public void EncryptDecryptStringAesSuccessTest()
		{
			string plainText = "SomeValue";
			string sharedSecret = "SuperSecret1234";
			string salt = "SomeSalt1234";
			string cipher = Crypto.EncryptStringAes(plainText, sharedSecret, salt);

			Assert.IsTrue(!string.IsNullOrEmpty(cipher));
			Assert.AreEqual(plainText, Crypto.DecryptStringAes(cipher, sharedSecret, salt));
		}

		[TestMethod()]
		public void EncryptDecryptStringAesFailsWithBadSaltTest()
		{
			string plainText = "SomeValue";
			string sharedSecret = "SuperSecret1234";
			string salt = "SomeSalt1234";
			string cipher = Crypto.EncryptStringAes(plainText, sharedSecret, salt);

			Assert.AreNotEqual(plainText, Crypto.DecryptStringAes(cipher, sharedSecret, salt + "0"));
		}

		[TestMethod()]
		public void EncryptDecryptStringAesFailsWithBadSecretTest()
		{
			string plainText = "SomeValue";
			string sharedSecret = "SuperSecret1234";
			string salt = "SomeSalt1234";
			string cipher = Crypto.EncryptStringAes(plainText, sharedSecret, salt);

			Assert.AreNotEqual(plainText, Crypto.DecryptStringAes(cipher, sharedSecret + "0", salt));
		}

		[TestMethod()]
		public void EncryptDecryptStringAesFailsWithBadCipherTest()
		{
			string plainText = "SomeValue";
			string sharedSecret = "SuperSecret1234";
			string salt = "SomeSalt1234";
			string cipher = Crypto.EncryptStringAes(plainText, sharedSecret, salt);

			Assert.AreNotEqual(plainText, Crypto.DecryptStringAes(cipher + "0", sharedSecret, salt));
		}
	}
}