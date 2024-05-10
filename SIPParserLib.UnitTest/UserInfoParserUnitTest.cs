using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLib;
using SIPParserLib.Parsers;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class UserInfoParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new UserInfoParser(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}

		


		[TestMethod]
		public void ParseShouldNotParseInvalidUserInfo()
		{
			UserInfo? value;
			bool result;

			UserInfoParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new UserInfoParser(logger);

			// invalid method
			result = parser.Parse("",out value,true);
			Assert.IsNull(value);
			Assert.IsFalse(result);
			Assert.AreEqual(1, logger.ErrorCount);

		}

		[TestMethod]
		public void ParseShouldParseUserInfoWithoutPassword()
		{
			UserInfo? value;
			bool result;

			UserInfoParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new UserInfoParser(logger);

			result = parser.Parse("+33140143960", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33140143960", value.User);
			Assert.IsNull(value.Password);
		}

		[TestMethod]
		public void ParseShouldParseUserInfoWithPassword()
		{
			UserInfo? value;
			bool result;

			UserInfoParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new UserInfoParser(logger);

			result = parser.Parse("+33140143960:Pass1234", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("+33140143960", value.User);
			Assert.AreEqual("Pass1234", value.Password);
		}

		[TestMethod]
		public void ParseShouldParseEscapedUserInfoWithPassword()
		{
			UserInfo? value;
			bool result;

			UserInfoParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new UserInfoParser(logger);

			result = parser.Parse("alice%40example.com:Pass1234", out value, true);
			Assert.IsNotNull(value);
			Assert.IsTrue(result);
			Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			Assert.AreEqual("alice@example.com", value.User);
			Assert.AreEqual("Pass1234", value.Password);
		}


	}
}
