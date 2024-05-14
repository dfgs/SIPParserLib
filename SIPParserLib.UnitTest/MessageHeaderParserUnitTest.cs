using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;

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
			Assert.ThrowsException<ArgumentNullException>(() => new MessageHeaderParser(null, Mock.Of<IStructStringParser<Address>>(), Mock.Of<IClassStringParser<ViaParameter>>(), Mock.Of<IClassStringParser<URI>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new MessageHeaderParser(NullLogger.Instance, null, Mock.Of<IClassStringParser<ViaParameter>>(), Mock.Of<IClassStringParser<URI>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new MessageHeaderParser(NullLogger.Instance, Mock.Of<IStructStringParser<Address>>(), null, Mock.Of<IClassStringParser<URI>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new MessageHeaderParser(NullLogger.Instance, Mock.Of<IStructStringParser<Address>>(), Mock.Of<IClassStringParser<ViaParameter>>(), null));
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

		[TestMethod]
		public void ParseShouldParseViaHeader()
		{
			MessageHeader? value;
			bool result;
			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.Parse("Via: SIP/2.0/UDP 172.20.54.2:5060;branch=z9hG4bKe4iuv7202ocnobm5dhf0.1", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ViaHeader));
			Assert.AreEqual("Via", value.Name);
			Assert.AreEqual(1, ((ViaHeader)value).Parameters.Length);
			Assert.IsInstanceOfType(((ViaHeader)value).Parameters[0], typeof(ViaBranch));
		}

		[TestMethod]
		public void ParseShouldParseFromHeader()
		{
			MessageHeader? value;
			bool result;
			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.Parse("From: <sip:+262262595179@172.20.54.2;user=phone>;tag=SD58d3901-U2xemg", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(FromHeader));
			Assert.AreEqual("From", value.Name);
			Assert.IsNotNull(((FromHeader)value).Value);
		}

		[TestMethod]
		public void ParseShouldParseToHeader()
		{
			MessageHeader? value;
			bool result;
			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.Parse("To: <sip:+33156716199@172.20.52.20;user=phone>;tag=0086183E-0CA7-14B4-8A11-3E69230AAA77-6011503", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ToHeader));
			Assert.AreEqual("To", value.Name);
			Assert.IsNotNull(((ToHeader)value).Value);
		}
		[TestMethod]
		public void ParseShouldParseReferToHeader()
		{
			MessageHeader? value;
			bool result;
			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.Parse("Refer-To: <sip:+33156716199@172.20.52.20;user=phone>;tag=0086183E-0CA7-14B4-8A11-3E69230AAA77-6011503", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ReferToHeader));
			Assert.AreEqual("Refer-To", value.Name);
			Assert.IsNotNull(((ReferToHeader)value).Value);
		}
		[TestMethod]
		public void ParseShouldParseReferedByHeader()
		{
			MessageHeader? value;
			bool result;
			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.Parse("Referred-By: <sip:+33156716199@172.20.52.20;user=phone>;tag=0086183E-0CA7-14B4-8A11-3E69230AAA77-6011503", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ReferredByHeader));
			Assert.AreEqual("Referred-By", value.Name);
			Assert.IsNotNull(((ReferredByHeader)value).Value);

			result = parser.Parse("Referred-By: sip:+991002008355040@10.91.219.12:5060", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ReferredByHeader));
			Assert.AreEqual("Referred-By", value.Name);
			Assert.IsNotNull(((ReferredByHeader)value).Value);
		}
		[TestMethod]
		public void ParseShouldParseCallIDHeader()
		{
			MessageHeader? value;
			bool result;
			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.Parse("Call-ID: SD58d3901-c47acaf5095a2a40ef124c5d785fa35f-v300g000J0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(CallIDHeader));
			Assert.AreEqual("Call-ID", value.Name);
			Assert.AreEqual("SD58d3901-c47acaf5095a2a40ef124c5d785fa35f-v300g000J0", ((CallIDHeader)value).Value);
		}
		[TestMethod]
		public void ParseShouldParseCSeqHeader()
		{
			MessageHeader? value;
			bool result;
			MessageHeaderParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new MessageHeaderParser(logger);

			result = parser.Parse("CSeq: 795666 INVITE", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(CSeqHeader));
			Assert.AreEqual("CSeq", value.Name);
			Assert.AreEqual("795666 INVITE", ((CSeqHeader)value).Value);
		}

	}
}
