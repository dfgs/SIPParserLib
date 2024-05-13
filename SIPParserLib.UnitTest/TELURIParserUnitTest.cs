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
	public class TELURIParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new TELURIParser(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}



		[TestMethod]
		public void ParseShouldNotParseInvalidTELURI()
		{
			TELURL? value;
			bool result;
			TELURIParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new TELURIParser(logger);

			// invalid method
			result= parser.Parse("sip:+33140143960",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);

			// invalid parameter
			result= parser.Parse("tel:", out value, true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void ParseShouldParseValidTELURI()
		{
			TELURL? value;
			bool result;
			TELURIParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new TELURIParser(logger);

			result = parser.Parse("tel:+33140143960", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33140143960", value.PhoneNumber );

		}





	}
}
