using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class URIGrammarUnitTest
	{
		[TestMethod]
		public void ShouldParseUserInfoWithPassword()
		{
			UserInfo userInfo;

			userInfo = URIGrammar.UserInfo.Parse("Homer:Password");
			Assert.IsNotNull(userInfo);
			Assert.AreEqual("Homer", userInfo.User);
			Assert.AreEqual("Password", userInfo.Password);
		}
		[TestMethod]
		public void ShouldParseUserInfoWithoutPassword()
		{
			UserInfo userInfo;

			userInfo = URIGrammar.UserInfo.Parse("Homer");
			Assert.IsNotNull(userInfo);
			Assert.AreEqual("Homer", userInfo.User);
			Assert.IsNull(userInfo.Password);
		}



	}
}
