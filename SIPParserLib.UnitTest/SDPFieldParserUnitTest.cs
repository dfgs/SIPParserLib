using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SDPFieldParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new SDPFieldParser(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}

		


		[TestMethod]
		public void ParseShouldNotParseInvalidSDPField()
		{
			SDPField? value;
			bool result;

			SDPFieldParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid string
			result = parser.Parse("",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid name
			result = parser.Parse("=1234", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid name
			result = parser.Parse("ab=1234", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid value
			result = parser.Parse("param=", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid version
			result = parser.Parse("v=abc", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid origin
			result = parser.Parse("o=- 1188361355 1189881855 IN IP4", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid connection
			result = parser.Parse("c=IN IP4", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid time1
			result = parser.Parse("t=1324", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid time2
			result = parser.Parse("t=1324 abc", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid media1
			result = parser.Parse("m=audio abc RTP/AVP 8 101", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid media2
			result = parser.Parse("m=audio 51992 RTP/AVP", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);
			// invalid attribute
			result = parser.Parse("a=fmtp:", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

		}


		/*[TestMethod]
		public void ParseShouldParseSDPFieldWithoutValue()
		{
			SDPField? value;
			bool result;

			SDPFieldParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);

			result = parser.Parse("param", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("param", value.Value.Name);
			Assert.IsNull(value.Value.Value);
		}*/

		[TestMethod]
		public void ParseShouldParseValidSDPField()
		{
			SDPField? value;
			bool result;
			SDPFieldParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SDPFieldParser(logger);

			result = parser.Parse("p=ttl", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(value, typeof(CustomSDPField));
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("p", value.Name);
			Assert.AreEqual("ttl", ((CustomSDPField)value).Value);

			result = parser.Parse("v=2", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(VersionField));
			Assert.AreEqual("v", value.Name);
			Assert.AreEqual((byte)2, ((VersionField)value).Value);

			result = parser.Parse("o=- 1188361355 1189881855 IN IP4 172.20.52.20", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(OriginField));
			Assert.AreEqual("o", value.Name);
			Assert.AreEqual("-", ((OriginField)value).UserName);
			Assert.AreEqual("1188361355", ((OriginField)value).SessionID);
			Assert.AreEqual("1189881855", ((OriginField)value).SessionVersion);
			Assert.AreEqual("IN", ((OriginField)value).NetworkType);
			Assert.AreEqual("IP4", ((OriginField)value).AddressType);
			Assert.AreEqual("172.20.52.20", ((OriginField)value).Address);

			result = parser.Parse("s=phone-call", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(SessionNameField));
			Assert.AreEqual("s", value.Name);
			Assert.AreEqual("phone-call", ((SessionNameField)value).Value);

			result = parser.Parse("c=IN IP4 172.20.52.20", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ConnectionField));
			Assert.AreEqual("c", value.Name);
			Assert.AreEqual("IN", ((ConnectionField)value).NetworkType);
			Assert.AreEqual("IP4", ((ConnectionField)value).AddressType);
			Assert.AreEqual("172.20.52.20", ((ConnectionField)value).Address);

			result = parser.Parse("i=ttl", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(SessionInformationField));
			Assert.AreEqual("i", value.Name);
			Assert.AreEqual("ttl", ((SessionInformationField)value).Value);

			result = parser.Parse("t=100 200", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(TimingField));
			Assert.AreEqual("t", value.Name);
			Assert.AreEqual((uint)100, ((TimingField)value).StartTime);
			Assert.AreEqual((uint)200, ((TimingField)value).StopTime);

			result = parser.Parse("m=audio 51992 RTP/AV 8 101", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(MediaField));
			Assert.AreEqual("m", value.Name);
			Assert.AreEqual("audio", ((MediaField)value).Media);
			Assert.AreEqual((ushort)51992, ((MediaField)value).Port);
			Assert.AreEqual("RTP/AV", ((MediaField)value).Protocol);
			Assert.AreEqual("8 101", ((MediaField)value).Format);

			result = parser.Parse("a=ptime:20", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(AttributeField));
			Assert.AreEqual("a", value.Name);
			Assert.AreEqual("ptime", ((AttributeField)value).Value.Name);
			Assert.AreEqual("20", ((AttributeField)value).Value.Value);

			result = parser.Parse("a=sendrecv", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(AttributeField));
			Assert.AreEqual("a", value.Name);
			Assert.AreEqual("sendrecv", ((AttributeField)value).Value.Name);
			Assert.AreEqual("", ((AttributeField)value).Value.Value);


		}





	}
}
