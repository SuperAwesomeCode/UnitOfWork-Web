using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests.Extensions
{
	[TestClass()]
	public class DirectoryExtensionsTest : BaseUnitTest
	{
		///<summary>
		///	A test for CreateDirectoryIfDoesntExists
		///</summary>
		[TestMethod()]
		public void CreateDirectoryIfDoesntExistsDoesNotFailWithExistingDirectoryTest()
		{
			string directory = Directory.GetCurrentDirectory();
			Assert.IsTrue(Directory.Exists(directory));
			
			DirectoryExtensions.CreateDirectoryIfDoesntExists(directory);
			Assert.IsTrue(Directory.Exists(directory));
		}

		///<summary>
		///	A test for CreateDirectoryIfDoesntExists
		///</summary>
		[TestMethod()]
		public void CreateDirectoryIfDoesntExistsDoesNotFailWithNotExistingDirectoryTest()
		{
			string directory = Directory.GetCurrentDirectory();
			while (Directory.Exists(directory))
			{
				directory += "/CreateDirectoryIfDoesntExistsDoesNotFailWithNotExistingDirectoryTest/";
			}

			DirectoryExtensions.CreateDirectoryIfDoesntExists(directory);
			Assert.IsTrue(Directory.Exists(directory));

			try
			{
				Directory.Delete(directory);
			}
			catch
			{
				//Just trying to be nice
			}
		}
	}
}
