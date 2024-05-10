﻿using LogLib;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class GenericClassParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new GenericClassParser<Request>(null, (value) => null));
			Assert.ThrowsException<ArgumentNullException>(() => new GenericClassParser<Request>(NullLogger.Instance, null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}

		[TestMethod]
		public void ParseShouldLogErrorIfLineIsNull()
		{
			GenericClassParser<Request> parser;
			DebugLogger logger;
			bool result;
			Request? value;

			logger = new DebugLogger();
			parser = new GenericClassParser<Request>(logger,(value)=>null);


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
			GenericClassParser<Request> parser;
			DebugLogger logger;
			bool result;
			Request? value;

			logger = new DebugLogger();
			parser = new GenericClassParser<Request>(logger, (value) => null);


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
			GenericClassParser<Request> parser;
			DebugLogger logger;
			bool result;
			Request? value;

			logger = new DebugLogger();
			parser = new GenericClassParser<Request>(logger, (value) => null);


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
			GenericClassParser<Request> parser;
			DebugLogger logger;
			bool result;
			Request? value;

			logger = new DebugLogger();
			parser = new GenericClassParser<Request>(logger, (value) => null);


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
			GenericClassParser<Request> parser;
			DebugLogger logger;
			bool result;
			Request? value;

			logger = new DebugLogger();
			parser = new GenericClassParser<Request>(logger, (value) => throw new ArgumentException("Line ErrorTest"));


#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			result = parser.Parse("123", out value, true);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);
			Assert.IsTrue(logger.LogsContainKeyWords(LogLevels.Error, "Line", "ErrorTest"));
		}

	}
}
