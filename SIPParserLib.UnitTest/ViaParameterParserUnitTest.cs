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
	public class ViaParameterParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new ViaParameterParser(null ,Mock.Of<IStructStringParser<HostPort>>() ));
			Assert.ThrowsException<ArgumentNullException>(() => new ViaParameterParser(NullLogger.Instance, null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}




		[TestMethod]
		public void ParseShouldNotParseInvalidViaParameter()
		{
			ViaParameter? value;
			bool result;

			ViaParameterParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new ViaParameterParser(logger);
			// invalid string
			result = parser.Parse("",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new ViaParameterParser(logger);
			// invalid name
			result = parser.Parse("=1234", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new ViaParameterParser(logger);
			// invalid value
			result = parser.Parse("param1=", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);
		}
		

		[TestMethod]
		public void ParseShouldParseViaParameterWithoutValue()
		{
			ViaParameter? value;
			bool result;

			ViaParameterParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new ViaParameterParser(logger);

			result = parser.Parse("pa", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("pa", value.Name);
			Assert.IsNull(((ViaCustomParameter)value).Value);
		}

		[TestMethod]
		public void ParseShouldParseViaParameterWithValue()
		{
			ViaParameter? value;
			bool result;
			ViaParameterParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new ViaParameterParser(logger);

			result = parser.Parse("pa=ttl", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ViaCustomParameter));
			Assert.AreEqual("pa", value.Name);
			Assert.AreEqual("ttl", ((ViaCustomParameter)value).Value);


			result = parser.Parse("hidden=val1", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ViaHidden));
			Assert.AreEqual("hidden", value.Name);
			Assert.AreEqual("", ((ViaHidden)value).Value);

			result = parser.Parse("maddr=val1", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ViaMAddr));
			Assert.AreEqual("maddr", value.Name);
			Assert.AreEqual("val1", ((ViaMAddr)value).Value);

			result = parser.Parse("received=192.168.1.5", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ViaReceived));
			Assert.AreEqual("received", value.Name);
			Assert.AreEqual("192.168.1.5", ((ViaReceived)value).Value.ToString());


			result = parser.Parse("ttl=128", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(ViaTTL));
			Assert.AreEqual("ttl", value.Name);
			Assert.AreEqual((byte)128, ((ViaTTL)value).Value);


		}





		[TestMethod]
		public void ParseShouldParseAllParameters()
		{
			ViaParameter[]? value;
			bool result;
			ViaParameterParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new ViaParameterParser(logger);

			result = parser.ParseAll("param1=ttl;param2;param3=test",';',out value,true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual(3, value.Length);
			Assert.AreEqual("param1", value[0].Name);
			Assert.AreEqual("ttl", ((ViaCustomParameter)value[0]).Value);
			Assert.AreEqual("param2", value[1].Name);
			Assert.IsNull( ((ViaCustomParameter)value[1]).Value);
			Assert.AreEqual("param3", value[2].Name);
			Assert.AreEqual("test", ((ViaCustomParameter)value[2]).Value);
		}//*/


	}
}
