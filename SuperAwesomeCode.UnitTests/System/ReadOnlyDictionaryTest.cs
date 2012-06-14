using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperAwesomeCode.UnitTests;

namespace System.Collections.Generic
{
	[TestClass()]
	public class ReadOnlyDictionaryTest : BaseUnitTest
	{
		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void EnusureReadOnlyDictionaryIsReadOnly()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{ "key1", "value1" },
				{ "key2", "value2" },
				{ "key3", "value3" },
			};

			ReadOnlyDictionary<string, string> readOnly = new ReadOnlyDictionary<string, string>(dictionary);

			//TODO: Determine if this functionality should remain. 
			//Should the ReadOnlyDictionary copy values or use same object that is was created with?
			dictionary["key1"] = "other";
			Assert.AreEqual("other", readOnly["key1"]);

			//throws NotSupportedException
			readOnly["key1"] = readOnly["key1"] + "_";
		}
	}
}