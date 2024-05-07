using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SIPMessageParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new SIPMessageParser(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}

		[TestMethod]
		public void ParseShouldLogFatalAndThrowExceptionIfParameterIsNull()
		{
			SIPMessageParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SIPMessageParser(logger);

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => parser.Parse(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

			Assert.AreEqual(1, logger.FatalCount);
			Assert.IsTrue(logger.LogsContainKeyWords(LogLevels.Fatal,"Stream","defined"));
		}

	}
}
