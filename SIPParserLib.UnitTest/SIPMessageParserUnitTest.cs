using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;
using System.Reflection;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SIPMessageParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new SIPMessageParser(null, Mock.Of<IClassStringParser<RequestLine>>(), Mock.Of<IClassStringParser<StatusLine>>(), Mock.Of<IClassStringParser<MessageHeader>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SIPMessageParser(NullLogger.Instance, null, Mock.Of<IClassStringParser<StatusLine>>(), Mock.Of<IClassStringParser<MessageHeader>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SIPMessageParser(NullLogger.Instance, Mock.Of<IClassStringParser<RequestLine>>(), null, Mock.Of<IClassStringParser<MessageHeader>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SIPMessageParser(NullLogger.Instance, Mock.Of<IClassStringParser<RequestLine>>(), Mock.Of<IClassStringParser<StatusLine>>(), null ));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}



		[DataTestMethod]
		[DataRow("SIPParserLib.UnitTest.SIPMessages.RequestInvite1.txt")]
		public void ParseShouldParseRequest(string ResourceName)
		{
			SIPMessage? message;

			SIPMessageParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SIPMessageParser(logger);
			
			using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName))
			{
				if (stream == null) Assert.Fail("Resource not found");
				message = parser.Parse(stream);
				Assert.IsNotNull(message);
				Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
				Assert.IsInstanceOfType(message, typeof(Request));
			}

		}

		[DataTestMethod]
		[DataRow("SIPParserLib.UnitTest.SIPMessages.RequestIncompleteInvite1.txt")]
		public void ParseShouldNotParseIncompleteRequest(string ResourceName)
		{
			SIPMessage? message;

			SIPMessageParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SIPMessageParser(logger);

			using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName))
			{
				if (stream == null) Assert.Fail("Resource not found");
				message = parser.Parse(stream);
				Assert.IsNull(message);
				Assert.AreNotEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			}

		}



		[DataTestMethod]
		[DataRow("SIPParserLib.UnitTest.SIPMessages.ResponseTrying1.txt")]
		public void ParseShouldParseResponse(string ResourceName)
		{
			SIPMessage? message;

			SIPMessageParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SIPMessageParser(logger);

			using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName))
			{
				if (stream == null) Assert.Fail("Resource not found");
				message = parser.Parse(stream);
				Assert.IsNotNull(message);
				Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
				Assert.IsInstanceOfType(message, typeof(Response));
			}
		}

		[DataTestMethod]
		[DataRow("SIPParserLib.UnitTest.SIPMessages.ResponseIncompleteTrying1.txt")]
		public void ParseShouldNotParseIncompleteResponse(string ResourceName)
		{
			SIPMessage? message;

			SIPMessageParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SIPMessageParser(logger);

			using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName))
			{
				if (stream == null) Assert.Fail("Resource not found");
				message = parser.Parse(stream);
				Assert.IsNull(message);
				Assert.AreNotEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			}

		}

	}
}
