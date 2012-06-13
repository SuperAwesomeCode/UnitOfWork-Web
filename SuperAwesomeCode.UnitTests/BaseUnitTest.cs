using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests
{
	/// <summary>Base class for unit tests.</summary>
	public class BaseUnitTest
	{
		/// <summary>Gets or sets the test context.</summary>
		/// <value>The test context.</value>
		public TestContext TestContext { get; set; }
	}
}
