using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class GenericStructParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new GenericStructParser<int>(null, (value) => 1));
			Assert.ThrowsException<ArgumentNullException>(() => new GenericStructParser<int>(NullLogger.Instance, null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}

		[TestMethod]
		public void ParseShouldLogErrorIfLineIsNull()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger,(value)=>1);


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.Parse((string)null, out value, true);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);
			Assert.IsTrue(logger.LogsContainKeyWords(LogLevels.Error, "line", "null"));
		}

		[TestMethod]
		public void ParseShouldNotLogErrorIfLineIsNull()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger, (value) => 1);


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.Parse((string)null, out value, false);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount+logger.WarningCount+logger.FatalCount);
			Assert.IsNull(value);
		}

		[TestMethod]
		public void ParseShouldLogErrorIfGroupIsNull()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger, (value) => 1);


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.Parse((Group)null, out value, true);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);
			Assert.IsTrue(logger.LogsContainKeyWords(LogLevels.Error, "group", "null"));
		}

		[TestMethod]
		public void ParseShouldNotLogErrorIfGroupIsNull()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger, (value) => 1);


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.Parse((Group)null, out value, false);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value);
		}
		[TestMethod]
		public void ParseShouldLogErrorIfConvertFunctionFails()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger, (value) => throw new ArgumentException("Line ErrorTest"));


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.Parse("123", out value, true);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);
			Assert.IsTrue(logger.LogsContainKeyWords(LogLevels.Error, "Line", "ErrorTest"));
		}




		[TestMethod]
		public void ParseAllShouldLogErrorIfLineIsNull()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int[]? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger, (value) => 1);


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.ParseAll((string)null,';', out value, true);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);
			Assert.IsTrue(logger.LogsContainKeyWords(LogLevels.Error, "line", "null"));
		}

		[TestMethod]
		public void ParseAllShouldNotLogErrorIfLineIsNull()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int[]? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger, (value) => 1);


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.ParseAll((string)null, ';', out value, false);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value);
		}

		[TestMethod]
		public void ParseAllShouldLogErrorIfGroupIsNull()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int[]? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger, (value) => 1);


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.ParseAll((Group)null, ';', out value, true);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);
			Assert.IsTrue(logger.LogsContainKeyWords(LogLevels.Error, "group", "null"));
		}

		[TestMethod]
		public void ParseAllShouldNotLogErrorIfGroupIsNull()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int[]? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger, (value) => 1);


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.ParseAll((Group)null, ';', out value, false);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.IsNull(value);
		}

		[TestMethod]
		public void ParseAllShouldLogErrorIfConvertFunctionFails()
		{
			GenericStructParser<int> parser;
			DebugLogger logger;
			bool result;
			int[]? value;

			logger = new DebugLogger();
			parser = new GenericStructParser<int>(logger, (value) => throw new ArgumentException("Line ErrorTest"));


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.ParseAll("123;325", ';',out value, true);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsFalse(result);
			Assert.AreEqual(2, logger.ErrorCount);
			Assert.IsTrue(logger.LogsContainKeyWords(LogLevels.Error, "Line", "ErrorTest"));
		}


	}
}
