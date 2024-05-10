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
	public class RequestURIParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new RequestURIParser(null, Mock.Of<IClassStringParser<UserInfo>>() , Mock.Of<IStructStringParser<HostPort>>(), Mock.Of<IStructStringParser<URLParameter>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new RequestURIParser(NullLogger.Instance, null, Mock.Of<IStructStringParser<HostPort>>(), Mock.Of<IStructStringParser<URLParameter>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new RequestURIParser(NullLogger.Instance, Mock.Of<IClassStringParser<UserInfo>>(), null, Mock.Of<IStructStringParser<URLParameter>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new RequestURIParser(NullLogger.Instance, Mock.Of<IClassStringParser<UserInfo>>(), Mock.Of<IStructStringParser<HostPort>>(),null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}



		[TestMethod]
		public void ParseShouldNotParseInvalidRequestURI()
		{
			SIPURL? value;
			bool result;
			RequestURIParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new RequestURIParser(logger);

			// invalid method
			result= parser.Parse("tel:+33140143960@ecb.core.nord:5060;user=phone",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);

			// invalid parameter
			result= parser.Parse("sip:+33140143960@ecb.core.nord:5060;invalidparameter=", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ParseShouldParseValidRequestURI()
		{
			SIPURL? value;
			bool result;
			RequestURIParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new RequestURIParser(logger);

			result = parser.Parse("sip:+33140143960@ecb.core.nord:5061;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33140143960", value.UserInfo?.ToString());
			Assert.AreEqual("ecb.core.nord:5061", value.HostPort.ToString());
			Assert.AreEqual(1, value.Parameters?.Length);


			result = parser.Parse("sip:0243444265@100.127.1.1;transport=UDP;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("0243444265", value.UserInfo?.ToString());
			Assert.AreEqual("100.127.1.1", value.HostPort.ToString());
			Assert.AreEqual(2, value.Parameters?.Length);

			result = parser.Parse("sip:+33450050545@100.127.2.1;user=phone;BP=1086996", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33450050545", value.UserInfo?.ToString());
			Assert.AreEqual("100.127.2.1", value.HostPort.ToString());
			Assert.AreEqual(2, value.Parameters?.Length);

			result = parser.Parse("sip:+33663326291@185.221.88.177:5070;user=phone;sdp_iwf;transport=udp", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33663326291", value.UserInfo?.ToString());
			Assert.AreEqual("185.221.88.177:5070", value.HostPort.ToString());
			Assert.AreEqual(3, value.Parameters?.Length);

			result = parser.Parse("sip:100.127.2.1;transport=udp;sdp_iwf", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull( value.UserInfo);
			Assert.AreEqual("100.127.2.1", value.HostPort.ToString());
			Assert.AreEqual(2, value.Parameters?.Length);

			result = parser.Parse("sip:sip.pstnhub.microsoft.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.UserInfo);
			Assert.AreEqual("sip.pstnhub.microsoft.com", value.HostPort.ToString());
			Assert.IsNull(value.Parameters);

			result = parser.Parse("sip:10.1.240.40:5060", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.UserInfo);
			Assert.AreEqual("10.1.240.40:5060", value.HostPort.ToString());
			Assert.IsNull(value.Parameters);

			result = parser.Parse("sip:0426223338@10.7.240.40:5060;transport=UDP", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("0426223338", value.UserInfo?.ToString());
			Assert.AreEqual("10.7.240.40:5060", value.HostPort.ToString());
			Assert.AreEqual(1, value.Parameters?.Length);

			result = parser.Parse("sip:+33299725203@10.91.254.17:5060;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33299725203", value.UserInfo?.ToString());
			Assert.AreEqual("10.91.254.17:5060", value.HostPort.ToString());
			Assert.AreEqual(1, value.Parameters?.Length);

			result = parser.Parse("sip:10.223.161.1:5060;x-i=89f2cb50-8632-4279-b9c1-5d6ae444cd32;x-c=ccf72508a8535634bc4e23f5cd943e76/s/1/3871d959183a4c6bb8adc84aa1d12907;transport=udp", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.UserInfo);
			Assert.AreEqual("10.223.161.1:5060", value.HostPort.ToString());
			Assert.AreEqual(3, value.Parameters?.Length);


		}





	}
}
