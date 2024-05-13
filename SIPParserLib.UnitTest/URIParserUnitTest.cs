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
			Assert.IsInstanceOfType(value, typeof(SIPURL));

			result = parser.Parse("tel:+33140143960", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsInstanceOfType(value, typeof(TELURL));





		}





	}
}
