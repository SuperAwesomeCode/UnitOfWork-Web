using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests
{
	[TestClass()]
	public class GuardTest : BaseUnitTest
	{
		[TestMethod()]
		public void AgainstNullTest()
		{
			this.VerifyAgainstNull(null, null, null);
			this.VerifyAgainstNull(null, null, "zz");
			this.VerifyAgainstNull(null, "yy", null);
			this.VerifyAgainstNull(null, "yy", "zz");
			this.VerifyAgainstNull("xx", null, null);
			this.VerifyAgainstNull("xx", null, "zz");
			this.VerifyAgainstNull("xx", "yy", null);
			this.VerifyAgainstNull("xx", "yy", "zz");
		}

		[TestMethod()]
		public void AgainstNullOrEmptyTest()
		{
			this.VerifyAgainstNullOrEmpty(null, null, null);
			this.VerifyAgainstNullOrEmpty(null, null, "zz");
			this.VerifyAgainstNullOrEmpty(null, "yy", null);
			this.VerifyAgainstNullOrEmpty(null, "yy", "zz");
			this.VerifyAgainstNullOrEmpty(string.Empty, null, null);
			this.VerifyAgainstNullOrEmpty(string.Empty, null, "zz");
			this.VerifyAgainstNullOrEmpty(string.Empty, "yy", null);
			this.VerifyAgainstNullOrEmpty(string.Empty, "yy", "zz");
			this.VerifyAgainstNullOrEmpty("xx", null, null);
			this.VerifyAgainstNullOrEmpty("xx", null, "zz");
			this.VerifyAgainstNullOrEmpty("xx", "yy", null);
			this.VerifyAgainstNullOrEmpty("xx", "yy", "zz");
		}

		[TestMethod()]
		public void ThrowArgumentNullExceptionTest()
		{
			this.VerifyThrowArgumentNullException(null, null);
			this.VerifyThrowArgumentNullException(null, "yy");
			this.VerifyThrowArgumentNullException("xx,", null);
			this.VerifyThrowArgumentNullException("xx", "yy");
		}

		private void VerifyAgainstNull(object value, string parameterName, string messageName)
		{
			string parameteters = string.Format(
				"Parameters: ({0}, {1}, {2}) - ",
				value == null ? "null" : value.ToString(),
				parameterName == null ? "null" : parameterName,
				messageName == null ? "null" : messageName);

			try
			{
				Guard.AgainstNull(value, parameterName, messageName);
				Assert.IsNotNull(value, parameteters + "Value was null and ArgumentNullException was not thrown.");
			}
			catch (ArgumentNullException exception)
			{
				Assert.AreEqual(
					exception.ParamName,
					parameterName,
					parameteters + "Exception.ParmName does not equal parameterName.");

				if (!string.IsNullOrEmpty(messageName))
				{
					Assert.IsTrue(
						exception.Message.StartsWith(messageName),
						parameteters + "Exception.Message does not start with messageName.");
				}
			}
		}

		private void VerifyAgainstNullOrEmpty(string value, string parameterName, string messageName)
		{
			string parameteters = string.Format(
				"Parameters: ({0}, {1}, {2}) - ",
				value == null ? "null" : value,
				parameterName == null ? "null" : parameterName,
				messageName == null ? "null" : messageName);

			try
			{
				Guard.AgainstNullOrEmpty(value, parameterName, messageName);

				Assert.IsFalse(string.IsNullOrEmpty(value), parameteters + "Value was NullOrEmpty and ArgumentNullException was not thrown.");
			}
			catch (ArgumentNullException exception)
			{
				Assert.AreEqual(
					exception.ParamName,
					parameterName,
					parameteters + "Exception.ParmName does not equal parameterName.");

				if (!string.IsNullOrEmpty(messageName))
				{
					Assert.IsTrue(
						exception.Message.StartsWith(messageName),
						parameteters + "Exception.Message does not start with messageName.");
				}
			}
		}

		private void VerifyThrowArgumentNullException(string parameterName, string messageName)
		{
			string parameteters = string.Format(
				"Parameters: ({0}, {1}) - ",
				parameterName == null ? "null" : parameterName,
				messageName == null ? "null" : messageName);

			try
			{
				Guard.ThrowArgumentNullException(parameterName, messageName);
				Assert.Fail(parameteters + "ArgumentNullException was not thrown.");
			}
			catch (ArgumentNullException exception)
			{
				Assert.AreEqual(
					exception.ParamName,
					parameterName,
					parameteters + "Exception.ParmName does not equal parameterName.");

				if (!string.IsNullOrEmpty(messageName))
				{
					Assert.IsTrue(
						exception.Message.StartsWith(messageName),
						parameteters + "Exception.Message does not start with messageName.");
				}
			}
		}
	}
}
