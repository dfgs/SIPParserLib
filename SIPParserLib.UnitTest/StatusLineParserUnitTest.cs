using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class StatusLineParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new StatusLineParser(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}
	


		[TestMethod]
		public void ParseShouldNotParseInvalidStatusLine()
		{
			StatusLine? value;
			bool result;

			StatusLineParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new StatusLineParser(logger);

			// invalid reason
			result = parser.Parse("SIP/2.0 100", out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);

			// invalid version
			result = parser.Parse("SIP/1.0 100 Trying", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);

			// invalid code
			result = parser.Parse("SIP/2.0 1001 Trying", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ParseShouldParseValidStatusLine()
		{
			StatusLine? value;
			bool result;

			StatusLineParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new StatusLineParser(logger);

			result = parser.Parse("SIP/2.0 100 Trying", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("Trying", value.Reason);
			Assert.AreEqual((ushort)100, value.StatusCode);

		}





	}
}
