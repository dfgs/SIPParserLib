using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class URIParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new URIParser(null, Mock.Of<IClassStringParser<SIPURL>>(), Mock.Of<IClassStringParser<TELURL>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new URIParser(NullLogger.Instance, null, Mock.Of<IClassStringParser<TELURL>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new URIParser(NullLogger.Instance, Mock.Of<IClassStringParser<SIPURL>>(), null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}



		[TestMethod]
		public void ParseShouldNotParseInvalidURI()
		{
			URI? value;
			bool result;
			URIParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new URIParser(logger);

			// invalid method
			result= parser.Parse("toto:+33140143960",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);

		}

		[TestMethod]
		public void ParseShouldParseValidURI()
		{
			URI? value;
			bool result;
			URIParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new URIParser(logger);

			result = parser.Parse("sip:+33140143960@ecb.core.nord:5061;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33140143960", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("ecb.core.nord:5061", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(1, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("tel:+33140143960", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33140143960", ((TELURL)value).PhoneNumber);

			result = parser.Parse("sip:j.doe@big.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("j.doe", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("big.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(0, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:j.doe:secret@big.com;transport=tcp", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("j.doe", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("secret@big.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(1, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:j.doe@big.com?subject=project", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("j.doe", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("big.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(1, ((SIPURL)value).Headers?.Length);

			result = parser.Parse("sip:+1-212-555-1212:1234@gateway.com;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+1-212-555-1212:1234", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("gateway.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(1, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:1212@gateway.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("1212", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("gateway.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(0, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:alice@10.1.2.3", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("alice", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("10.1.2.3", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(0, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:alice@example.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("alice", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("example.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(1, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:+33140143960@ecb.core.nord:5061;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33140143960", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("ecb.core.nord:5061", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(1, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:alice%40example.com@gateway.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("alice@example.com", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("gateway.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(0, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:alice@registrar.com;method=REGISTER", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("alice", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("registrar.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(1, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:big.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(((SIPURL)value).UserInfo);
			Assert.AreEqual("big.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(0, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:j.doe@big.com?subject=project&name=test", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("j.doe", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("big.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(2, ((SIPURL)value).Headers?.Length);

			result = parser.Parse("sip:0243444265@100.127.1.1;transport=UDP;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("0243444265", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("100.127.1.1", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(2, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("tel:+334556677", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+334556677", ((TELURL)value).PhoneNumber);

			result = parser.Parse("sip:+33450050545@100.127.2.1;user=phone;BP=1086996", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33450050545@100", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("100.127.2.1", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(2, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:+33140143960@ecb.core.nord:5061;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33140143960", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("ecb.core.nord:5061", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(1, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:100.127.2.1;transport=udp;sdp_iwf", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull( ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("100.127.2.1", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(2, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:+33549400033@ent.bouyguestelecom.fr;user=phone;eribindingid=1638919091564375;eribind-generated-at=10.79.21.198", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33549400033", ((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("ent.bouyguestelecom.fr", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(3, ((SIPURL)value).Parameters?.Length);

			result = parser.Parse("sip:sip.pstnhub.microsoft.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(((SIPURL)value).UserInfo?.ToString());
			Assert.AreEqual("sip:sip.pstnhub.microsoft.com", ((SIPURL)value).HostPort.ToString());
			Assert.AreEqual(0, ((SIPURL)value).Parameters?.Length);

		
	
	}





	}
}
