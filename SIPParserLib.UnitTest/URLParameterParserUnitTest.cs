using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class URLParameterParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new URLParameterParser(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}

		


		[TestMethod]
		public void ParseShouldNotParseInvalidURLParameter()
		{
			URLParameter? value;
			bool result;

			URLParameterParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new URLParameterParser(logger);
			// invalid string
			result = parser.Parse("",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new URLParameterParser(logger);
			// invalid name
			result = parser.Parse("=1234", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new URLParameterParser(logger);
			// invalid value
			result = parser.Parse("param=", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

		}
		/*[TestMethod]
		public void ParseShouldNotParseInvalidURLParameters()
		{
			URLParameter[]? result;

			URLParameterParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new URLParameterParser(logger);
			// invalid string
			result = parser.ParseAll("param=ttl;param=",';');
			Assert.IsNull(result);
			Assert.AreEqual(2, logger.ErrorCount);

			logger = new DebugLogger();
			parser = new URLParameterParser(logger);
			// invalid string
			result = parser.ParseAll("param=ttl;=1234", ';');
			Assert.IsNull(result);
			Assert.AreEqual(2, logger.ErrorCount);

		}//*/

		[TestMethod]
		public void ParseShouldParseURLParameterWithoutValue()
		{
			URLParameter? value;
			bool result;

			URLParameterParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new URLParameterParser(logger);

			result = parser.Parse("param", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("param", value.Value.Name);
			Assert.AreEqual("",value.Value.Value);
		}

		[TestMethod]
		public void ParseShouldParseURLParameterWithValue()
		{
			URLParameter? value;
			bool result;
			URLParameterParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new URLParameterParser(logger);

			result = parser.Parse("param=ttl", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("param", value.Value.Name);
			Assert.AreEqual("ttl", value.Value.Value);
		}


		/*[TestMethod]
		public void ParseShouldParseAllParameters()
		{
			URLParameter[]? result;

			URLParameterParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new URLParameterParser(logger);

			result = parser.ParseAll("param=ttl;param2;param3=test",';');
			Assert.IsNotNull(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual(3, result.Length);
			Assert.AreEqual("param", result[0].Name);
			Assert.AreEqual("ttl", result[0].Value);
			Assert.AreEqual("param2", result[1].Name);
			Assert.AreEqual("", result[1].Value);
			Assert.AreEqual("param3", result[2].Name);
			Assert.AreEqual("test", result[2].Value);
		}//*/


	}
}
