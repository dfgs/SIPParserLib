using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SIPMessageParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new SIPMessageParser(null, Mock.Of<IClassStringParser<RequestLine>>(), Mock.Of<IClassStringParser<MessageHeader>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SIPMessageParser(NullLogger.Instance, null, Mock.Of<IClassStringParser<MessageHeader>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SIPMessageParser(NullLogger.Instance, Mock.Of<IClassStringParser<RequestLine>>(), null ));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}



		[TestMethod]
		public void ParseShouldParseINVITE()
		{
			SIPMessage? message;

			SIPMessageParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SIPMessageParser(logger);
			message = parser.Parse(new MemoryStream());

			Assert.IsNotNull(message);
			Assert.Fail("TODO");
		}

		[TestMethod]
		public void ParseShouldParseACK()
		{
			SIPMessage? message;

			SIPMessageParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SIPMessageParser(logger);
			message = parser.Parse(new MemoryStream());

			Assert.IsNotNull(message);
			Assert.Fail("TODO");
		}



	}
}
