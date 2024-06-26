﻿using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;

using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SIPURIParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new SIPURIParser(null, Mock.Of<IClassStringParser<UserInfo>>() , Mock.Of<IStructStringParser<HostPort>>(), Mock.Of<IStructStringParser<URLParameter>>(), Mock.Of<IStructStringParser<URIHeader>>() ));
			Assert.ThrowsException<ArgumentNullException>(() => new SIPURIParser(NullLogger.Instance, null, Mock.Of<IStructStringParser<HostPort>>(), Mock.Of<IStructStringParser<URLParameter>>(), Mock.Of<IStructStringParser<URIHeader>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SIPURIParser(NullLogger.Instance, Mock.Of<IClassStringParser<UserInfo>>(), null, Mock.Of<IStructStringParser<URLParameter>>(), Mock.Of<IStructStringParser<URIHeader>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SIPURIParser(NullLogger.Instance, Mock.Of<IClassStringParser<UserInfo>>(), Mock.Of<IStructStringParser<HostPort>>(), null, Mock.Of<IStructStringParser<URIHeader>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new SIPURIParser(NullLogger.Instance, Mock.Of<IClassStringParser<UserInfo>>(), Mock.Of<IStructStringParser<HostPort>>(),  Mock.Of<IStructStringParser<URLParameter>>(),null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}



		[TestMethod]
		public void ParseShouldNotParseInvalidSIPURI()
		{
			SIPURL? value;
			bool result;
			SIPURIParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SIPURIParser(logger);

			// invalid method
			result= parser.Parse("tel:+3333333331@sbc.example.com:5060;user=phone",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);

			// invalid parameter
			result= parser.Parse("sip:+3333333331@sbc.example.com:5060;invalidparameter=", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ParseShouldParseValidSIPURI()
		{
			SIPURL? value;
			bool result;
			SIPURIParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SIPURIParser(logger);

			result = parser.Parse("sip:+3333333331@sbc.example.com:5061;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+3333333331", value.UserInfo?.ToString());
			Assert.AreEqual("sbc.example.com:5061", value.HostPort.ToString());
			Assert.AreEqual(1, value.Parameters?.Length);
			Assert.IsNull(value.Headers);


			result = parser.Parse("sip:+3333333331@10.10.10.1;transport=UDP;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+3333333331", value.UserInfo?.ToString());
			Assert.AreEqual("10.10.10.1", value.HostPort.ToString());
			Assert.AreEqual(2, value.Parameters?.Length);
			Assert.IsNull(value.Headers);

			result = parser.Parse("sip:+33450050545@10.10.10.12;user=phone;BP=1086996", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33450050545", value.UserInfo?.ToString());
			Assert.AreEqual("10.10.10.12", value.HostPort.ToString());
			Assert.AreEqual(2, value.Parameters?.Length);
			Assert.IsNull(value.Headers);

			result = parser.Parse("sip:+3333333332@10.10.10.11:5070;user=phone;sdp_iwf;transport=udp", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+3333333332", value.UserInfo?.ToString());
			Assert.AreEqual("10.10.10.11:5070", value.HostPort.ToString());
			Assert.AreEqual(3, value.Parameters?.Length);
			Assert.IsNull(value.Headers);

			result = parser.Parse("sip:10.10.10.12;transport=udp;sdp_iwf", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull( value.UserInfo);
			Assert.AreEqual("10.10.10.12", value.HostPort.ToString());
			Assert.AreEqual(2, value.Parameters?.Length);
			Assert.IsNull(value.Headers);

			result = parser.Parse("sip:sip.pstnhub.microsoft.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.UserInfo);
			Assert.AreEqual("sip.pstnhub.microsoft.com", value.HostPort.ToString());
			Assert.IsNull(value.Parameters);
			Assert.IsNull(value.Headers);

			result = parser.Parse("sip:10.10.10.13:5060", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.UserInfo);
			Assert.AreEqual("10.10.10.13:5060", value.HostPort.ToString());
			Assert.IsNull(value.Parameters);
			Assert.IsNull(value.Headers);

			result = parser.Parse("sip:0404040404@10.10.10.40:5060;transport=UDP", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("0404040404", value.UserInfo?.ToString());
			Assert.AreEqual("10.10.10.40:5060", value.HostPort.ToString());
			Assert.AreEqual(1, value.Parameters?.Length);
			Assert.IsNull(value.Headers);

			result = parser.Parse("sip:+3333333331@10.10.10.50:5060;user=phone", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+3333333331", value.UserInfo?.ToString());
			Assert.AreEqual("10.10.10.50:5060", value.HostPort.ToString());
			Assert.AreEqual(1, value.Parameters?.Length);
			Assert.IsNull(value.Headers);

			result = parser.Parse("sip:10.10.10.50:5060;x-i=89f2cb50-8632-4279-b9c1-5d6ae444cd32;x-c=ccf72508a8535634bc4e23f5cd943e76/s/1/3871d959183a4c6bb8adc84aa1d12907;transport=udp", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.UserInfo);
			Assert.AreEqual("10.10.10.50:5060", value.HostPort.ToString());
			Assert.AreEqual(3, value.Parameters?.Length);
			Assert.IsNull(value.Headers);

			result = parser.Parse("sip:j.doe@big.com?subject=project", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("j.doe", value.UserInfo?.ToString());
			Assert.AreEqual("big.com", value.HostPort.ToString());
			Assert.AreEqual(1, value.Headers?.Length);
			Assert.IsNull(value.Parameters);

			result = parser.Parse("sip:big.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.UserInfo);
			Assert.AreEqual("big.com", value.HostPort.ToString());
			Assert.IsNull(value.Parameters);

			result = parser.Parse("sip:j.doe@big.com?subject=project&name=test", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("j.doe", value.UserInfo?.ToString());
			Assert.AreEqual("big.com", value.HostPort.ToString());
			Assert.AreEqual(2, value.Headers?.Length);
			Assert.AreEqual("subject", value.Headers![0].Name);
			Assert.AreEqual("project", value.Headers![0].Value);
			Assert.AreEqual("name", value.Headers![1].Name);
			Assert.AreEqual("test", value.Headers![1].Value);

			result = parser.Parse("sip:j.doe@big.com;user=phone?subject=project&name=test", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("j.doe", value.UserInfo?.ToString());
			Assert.AreEqual("big.com", value.HostPort.ToString());
			Assert.AreEqual(1, value.Parameters?.Length);
			Assert.AreEqual("user", value.Parameters![0].Name);
			Assert.AreEqual("phone", value.Parameters![0].Value);
			Assert.AreEqual(2, value.Headers?.Length);
			Assert.AreEqual("subject", value.Headers![0].Name);
			Assert.AreEqual("project", value.Headers![0].Value);
			Assert.AreEqual("name", value.Headers![1].Name);
			Assert.AreEqual("test", value.Headers![1].Value);


			result = parser.Parse("sip:alice%40example.com@gateway.com", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("alice@example.com", value.UserInfo?.ToString());
			Assert.AreEqual("gateway.com", value.HostPort.ToString());
			Assert.IsNull(value.Headers);
			Assert.IsNull(value.Parameters);



		}





	}
}
