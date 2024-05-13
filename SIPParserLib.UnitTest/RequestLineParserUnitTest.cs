using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class RequestLineParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new RequestLineParser(null, Mock.Of<IClassStringParser<SIPURL>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new RequestLineParser(NullLogger.Instance, null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}



		[TestMethod]
		public void ParseShouldNotParseInvalidRequestLine()
		{
			RequestLine? value;
			bool result;

			RequestLineParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new RequestLineParser(logger);

			// invalid method
			result = parser.Parse("INVALID sip:+33140143960@ecb.core.nord:5060;user=phone SIP/2.0",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);

			// invalid version
			result = parser.Parse("INVITE sip:+33140143960@ecb.core.nord:5060;user=phone SIP/1.0", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);

			// invalid URI
			result = parser.Parse("INVITE xxx SIP/2.0", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ParseShouldParseValidRequestLine()
		{
			RequestLine? value;
			bool result;

			RequestLineParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new RequestLineParser(logger);

			result = parser.Parse("INVITE sip:+33140143960@ecb.core.nord:5060;user=phone SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("INVITE", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);
			Assert.AreEqual("ecb.core.nord:5060", ((SIPURL)value.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:+33140143960@ecb.core.nord:5060;user=phone", value.RequestURI.ToString());


			result = parser.Parse("INVITE sip:0243444265@100.127.1.1;transport=UDP;user=phone SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("INVITE", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);
			Assert.AreEqual("100.127.1.1", ((SIPURL)value.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:0243444265@100.127.1.1;transport=UDP;user=phone", value.RequestURI.ToString());

			result = parser.Parse("INVITE sip:+33450050545@100.127.2.1;user=phone;BP=1086996 SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("INVITE", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);
			Assert.AreEqual("100.127.2.1", ((SIPURL)value.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:+33450050545@100.127.2.1;user=phone;BP=1086996", value.RequestURI.ToString());

			result = parser.Parse("ACK sip:+33663326291@185.221.88.177:5060;user=phone;sdp_iwf;transport=udp SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("ACK", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);
			Assert.AreEqual("185.221.88.177:5060", ((SIPURL)value.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:+33663326291@185.221.88.177:5060;user=phone;sdp_iwf;transport=udp", value.RequestURI.ToString());

			result = parser.Parse("ACK sip:100.127.2.1;transport=udp;sdp_iwf SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("ACK", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);
			Assert.AreEqual("100.127.2.1", ((SIPURL)value.RequestURI).HostPort.ToString());
			Assert.AreEqual("sip:100.127.2.1;transport=udp;sdp_iwf", value.RequestURI.ToString());

			result = parser.Parse("OPTIONS sip:sip.pstnhub.microsoft.com SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("OPTIONS", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);
			Assert.AreEqual("sip:sip.pstnhub.microsoft.com", value.RequestURI.ToString());

			result = parser.Parse("NOTIFY sip:10.1.240.40:5060 SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("NOTIFY", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);
			Assert.AreEqual("sip:10.1.240.40:5060", value.RequestURI.ToString());

			result = parser.Parse("REFER sip:0426223338@10.7.240.40:5060;transport=UDP SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("REFER", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);
			Assert.AreEqual("sip:0426223338@10.7.240.40:5060;transport=UDP", value.RequestURI.ToString());

			result = parser.Parse("INVITE sip:+33299725203@10.91.254.17:5060;user=phone SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("INVITE", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);
			Assert.AreEqual("sip:+33299725203@10.91.254.17:5060;user=phone", value.RequestURI.ToString());

			result = parser.Parse("ACK sip:10.223.161.1:5060;x-i=89f2cb50-8632-4279-b9c1-5d6ae444cd32;x-c=ccf72508a8535634bc4e23f5cd943e76/s/1/3871d959183a4c6bb8adc84aa1d12907;transport=udp SIP/2.0", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("ACK", value.Method);
			Assert.AreEqual("SIP/2.0", value.SIPVersion);


		}





	}
}
