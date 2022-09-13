using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLib;
using System;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class CommonGrammarUnitTest
	{
		[TestMethod]
		public void ShouldParseQuotedString()
		{
			string value;

			value = CommonGrammar.QuotedString.Parse("\"Test\"");
			Assert.AreEqual("Test", value);

			value = CommonGrammar.QuotedString.Parse("\"Test\\\"Test\"");
			Assert.AreEqual("Test\"Test", value);
			value = CommonGrammar.QuotedString.Parse("\"Test Test\"", ' ');
			Assert.AreEqual("Test Test", value);
			value = CommonGrammar.QuotedString.Parse("\"A. G. Bell\"", ' ');
			Assert.AreEqual("A. G. Bell", value);
			value = CommonGrammar.QuotedString.Parse("\"TkUserFN0000197441 TkUserLN0000197441\"", ' ');
			Assert.AreEqual("TkUserFN0000197441 TkUserLN0000197441", value);
		}
		[TestMethod]
		public void ShouldParseToken()
		{
			string value;

			value = CommonGrammar.Token.Parse("Token-Value");
			Assert.AreEqual("Token-Value", value);

		}


	}
}
