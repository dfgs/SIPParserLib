using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class HostPortParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new HostPortParser(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}

		


		[TestMethod]
		public void ParseShouldNotParseInvalidHostPort()
		{
			HostPort? value;
			bool result;

			HostPortParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new HostPortParser(logger);
			// invalid method
			result = parser.Parse("",out value,true);
			Assert.IsFalse(result);
			Assert.IsNull(value);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new HostPortParser(logger);
			// invalid method
			result = parser.Parse("host:port", out value, true);
			Assert.IsFalse(result);
			Assert.IsNull(value);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new HostPortParser(logger);
			// invalid host
			result = parser.Parse(":1234", out value, true);
			Assert.IsFalse(result);
			Assert.IsNull(value);
			Assert.AreEqual(1, logger.ErrorCount);

		}

		[TestMethod]
		public void ParseShouldParseHostPortWithoutPort()
		{
			HostPort? value;
			bool result;
			HostPortParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new HostPortParser(logger);

			result = parser.Parse("192.168.1.2",out value,true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("192.168.1.2", value.Value.Host);
			Assert.IsNull(value.Value.Port);
		}

		[TestMethod]
		public void ParseShouldParseHostPortWithPort()
		{
			HostPort? value;
			bool result;
			HostPortParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new HostPortParser(logger);

			result = parser.Parse("192.168.1.2:5061",out value,true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("192.168.1.2", value.Value.Host);
			Assert.AreEqual((ushort)5061, value.Value.Port);
		}




	}
}
