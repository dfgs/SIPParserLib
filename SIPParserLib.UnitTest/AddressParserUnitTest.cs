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
	public class AddressParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new AddressParser(null, Mock.Of<IClassStringParser<URI>>(), Mock.Of<IStructStringParser<AddressParameter>>() , Mock.Of<IClassStringParser< UserInfo>>(), Mock.Of<IStructStringParser<HostPort>>() ));
			Assert.ThrowsException<ArgumentNullException>(() => new AddressParser(NullLogger.Instance, null, Mock.Of<IStructStringParser<AddressParameter>>(), Mock.Of<IClassStringParser<UserInfo>>(), Mock.Of<IStructStringParser<HostPort>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new AddressParser(NullLogger.Instance, Mock.Of<IClassStringParser<URI>>(), null, Mock.Of<IClassStringParser<UserInfo>>(), Mock.Of<IStructStringParser<HostPort>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new AddressParser(NullLogger.Instance, Mock.Of<IClassStringParser<URI>>(), Mock.Of<IStructStringParser<AddressParameter>>(), null, Mock.Of<IStructStringParser<HostPort>>()));
			Assert.ThrowsException<ArgumentNullException>(() => new AddressParser(NullLogger.Instance, Mock.Of<IClassStringParser<URI>>(), Mock.Of<IStructStringParser<AddressParameter>>(), Mock.Of<IClassStringParser<UserInfo>>(), null ));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}



		[TestMethod]
		public void ParseShouldNotParseInvalidAddress()
		{
			Address? value;
			bool result;
			AddressParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new AddressParser(logger);

			// missing char
			result= parser.Parse("sp:+3333333331@10.10.10.20;user=phone>;tag=0086183E-0CA7-14B4-8A11-3E69230AAA77-6011529", out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);

		}

		[TestMethod]
		public void ParseShouldParseValidAddress()
		{
			Address? value;
			bool result;
			AddressParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new AddressParser(logger);
			

			result = parser.Parse("\"A. G. Bell\" <sip:agb@bell-telephone.com>", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("A. G. Bell", value.Value.DisplayName);
			Assert.AreEqual("sip:agb@bell-telephone.com", value.Value.URI.ToString());
			Assert.IsNull(value.Value.Parameters); ;

			result = parser.Parse("<sip:+3333333332@10.10.10.11;user=phone>;tag=SDfefdf03-007302670000fdcf", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.IsNull( value.Value.DisplayName);
			Assert.AreEqual("sip:+3333333332@10.10.10.11;user=phone", value.Value.URI.ToString());
			Assert.AreEqual(1, value.Value.Parameters?.Length); ;

			result = parser.Parse("\"TkUserFN0000197441 TkUserLN0000197441\" <sip:+3333333331@provider.fr;user=phone;eribindingid=1638919091564375;eribind-generated-at=10.10.10.20>", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual("TkUserFN0000197441 TkUserLN0000197441", value.Value.DisplayName);
			Assert.AreEqual("sip:+3333333331@provider.fr;user=phone;eribindingid=1638919091564375;eribind-generated-at=10.10.10.20", value.Value.URI.ToString());
			Assert.IsNull(value.Value.Parameters); ;

			// parameters are binding to address (not URI), because ? is present
			result = parser.Parse("<sip:10.10.10.20:5060;transport=udp?Replaces=607cc119-f498-4e97-84ae-27d2223a8dd3@localhost;to-tag=SDdfsad99-72394F48-7E22-41AF-B84F-14EB0A6130F8-1939908;from-tag=2929199961609947131>", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.Value.DisplayName);
			Assert.AreEqual("sip:10.10.10.20:5060;transport=udp", value.Value.URI.ToString());
			Assert.AreEqual(3, value.Value.Parameters?.Length);

			result = parser.Parse("<sip:+3333333331@10.10.10.20:5060>;user=phone;transport=udp", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.Value.DisplayName);
			Assert.AreEqual("sip:+3333333331@10.10.10.20:5060", value.Value.URI.ToString());
			Assert.AreEqual(2, value.Value.Parameters?.Length);

			// parameters are binding to address (not URI, because ? is present
			result = parser.Parse("<sip:10.10.10.20:5060;transport=udp?Replaces=607cc119-f498-4e97-84ae-27d2223a8dd3%40localhost%3Bto-tag%3DSDdfsad99-72394F48-7E22-41AF-B84F-14EB0A6130F8-1939908%3Bfrom-tag%3D2929199961609947131>", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.Value.DisplayName);
			Assert.AreEqual("sip:10.10.10.20:5060;transport=udp", value.Value.URI.ToString());
			Assert.AreEqual(3, value.Value.Parameters?.Length);

			result = parser.Parse("Anonymous <sip:c8oqz84zk7z@privacy.org>", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("Anonymous", value.Value.DisplayName);
			Assert.AreEqual("sip:c8oqz84zk7z@privacy.org", value.Value.URI.ToString());
			Assert.IsNull(value.Value.Parameters); ;

			result = parser.Parse("<sip:+3333333331@10.10.10.50;user=phone>;tag=SD58d3901-U2xemg", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.Value.DisplayName);
			Assert.AreEqual("sip:+3333333331@10.10.10.50;user=phone", value.Value.URI.ToString());
			Assert.AreEqual(1, value.Value.Parameters?.Length); ;

			result = parser.Parse("sip:+3333333331@10.10.10.20:5060", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.Value.DisplayName);
			Assert.AreEqual("sip:+3333333331@10.10.10.20:5060", value.Value.URI.ToString());
			Assert.IsNull(value.Value.Parameters); ;

			result = parser.Parse("sip:+3333333331@10.10.10.20:5060;tag=SD58d3901-U2xemg", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value.Value.DisplayName);
			Assert.AreEqual("sip:+3333333331@10.10.10.20:5060", value.Value.URI.ToString());
			Assert.AreEqual(1, value.Value.Parameters?.Length); ;





		}





	}
}
