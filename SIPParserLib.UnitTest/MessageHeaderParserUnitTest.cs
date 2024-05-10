using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class MessageHeaderParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new MessageHeaderParser(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}

		


		[TestMethod]
		public void ParseShouldNotParseInvalidMessageHeader()
		{
			MessageHeader? value;
			bool result;

			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);
			// invalid string
			result = parser.Parse("",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);
			// invalid name
			result = parser.Parse(":1234", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			

		}
		

		[TestMethod]
		public void ParseShouldParseMessageHeaderWithoutValue()
		{
			MessageHeader? value;
			bool result;

			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.Parse("param:", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("param", value.Name);
			Assert.IsNull(((CustomHeader)value).Value);
		}

		[TestMethod]
		public void ParseShouldParseCustomMessageHeaderWithValue()
		{
			MessageHeader? value;
			bool result;
			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.Parse("param: ttl", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("param", value.Name);
			Assert.AreEqual("ttl", ((CustomHeader)value).Value);
		}



		[TestMethod]
		public void ParseShouldParseAllMessageHeaders()
		{
			MessageHeader[]? value;
			bool result;
			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.ParseAll("param: ttl;param2:;param3: test", ';', out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual(3, value.Length);
			Assert.AreEqual("param", value[0].Name);
			Assert.AreEqual("ttl", ((CustomHeader)value[0]).Value);
			Assert.AreEqual("param2", value[1].Name);
			Assert.IsNull(((CustomHeader)value[1]).Value);
			Assert.AreEqual("param3", value[2].Name);
			Assert.AreEqual("test", ((CustomHeader)value[2]).Value);
		}//*/


	}
}
