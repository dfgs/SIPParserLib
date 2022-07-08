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
			Assert.AreEqual("Homer", userInfo.User);
			Assert.AreEqual("Password", userInfo.Password);
		}
		[TestMethod]
		public void ShouldParseUserInfoWithoutPassword()
		{
			UserInfo userInfo;

			userInfo = URIGrammar.UserInfo.Parse("Homer");
			Assert.AreEqual("Homer", userInfo.User);
			Assert.IsNull(userInfo.Password);
		}
		[TestMethod]
		public void ShouldParseIPv4()
		{
			string ip;

			ip = URIGrammar.IPv4Address.Parse("192.168.1.2");
			Assert.AreEqual("192.168.1.2", ip);
		}

		[TestMethod]
		public void ShouldParseDomainLabel()
		{
			string domainLabel;

			domainLabel = URIGrammar.DomainLabel.Parse("S");
			Assert.AreEqual("S", domainLabel);
			domainLabel = URIGrammar.DomainLabel.Parse("Server");
			Assert.AreEqual("Server", domainLabel);
			domainLabel = URIGrammar.DomainLabel.Parse("Server1");
			Assert.AreEqual("Server1", domainLabel);
			domainLabel = URIGrammar.DomainLabel.Parse("Server-1");
			Assert.AreEqual("Server-1", domainLabel);
			domainLabel = URIGrammar.DomainLabel.Parse("Server-1-A");
			Assert.AreEqual("Server-1-A", domainLabel);
			domainLabel = URIGrammar.DomainLabel.Parse("Server-1A");
			Assert.AreEqual("Server-1A", domainLabel);
		}

		[TestMethod]
		public void ShouldParseHostname()
		{
			string hostname;

			hostname = URIGrammar.Hostname.Parse("Server1.domain.com");
			Assert.AreEqual("Server1.domain.com", hostname);
			hostname = URIGrammar.Hostname.Parse("Server1");
			Assert.AreEqual("Server1", hostname);
		}

		[TestMethod]
		public void ShouldParseURI1()
		{
			URI uri;
			
			uri = URIGrammar.RequestURI.Parse(Consts.URI1);
			Assert.AreEqual("j.doe", uri.UserInfo.User);
		}

	}
}
