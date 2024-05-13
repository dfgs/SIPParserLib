using LogLib;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SIPParserLib.Parsers;
using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SIPStringUnitTest
	{
		[TestMethod]
		public void ShouldUnescapeString()
		{
			string? value;


			value = SIPString.Unescape("alice%40example.com");
			Assert.AreEqual("alice@example.com", value);

			value = SIPString.Unescape("Replaces=607cc119-f498-4e97-84ae-27d2223a8dd3%40localhost%3Bto-tag%3DSDdfsad99-72394F48-7E22-41AF-B84F-14EB0A6130F8-1939908%3Bfrom-tag%3D2929199961609947131");
			Assert.AreEqual("Replaces=607cc119-f498-4e97-84ae-27d2223a8dd3@localhost;to-tag=SDdfsad99-72394F48-7E22-41AF-B84F-14EB0A6130F8-1939908;from-tag=2929199961609947131", value);

			Assert.IsNull(SIPString.Unescape(null));
		}






	}
}
