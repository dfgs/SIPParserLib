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
			domainLabel = URIGrammar.DomainLabel.Parse("1Server-1A");
			Assert.AreEqual("1Server-1A", domainLabel);
		}

		[TestMethod]
		public void ShouldParseTopLabel()
		{
			string topLabel;

			topLabel = URIGrammar.TopLabel.Parse("S");
			Assert.AreEqual("S", topLabel);
			topLabel = URIGrammar.TopLabel.Parse("Server");
			Assert.AreEqual("Server", topLabel);
			topLabel = URIGrammar.TopLabel.Parse("Server1");
			Assert.AreEqual("Server1", topLabel);
			topLabel = URIGrammar.TopLabel.Parse("Server-1");
			Assert.AreEqual("Server-1", topLabel);
			topLabel = URIGrammar.TopLabel.Parse("Server-1-A");
			Assert.AreEqual("Server-1-A", topLabel);
			topLabel = URIGrammar.TopLabel.Parse("Server-1A");
			Assert.AreEqual("Server-1A", topLabel);
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
		public void ShouldParseUserInfo()
		{
			UserInfo userInfo;

			userInfo = URIGrammar.UserInfo.Parse(Consts.User1);
			Assert.AreEqual("j.doe", userInfo.User);
			userInfo = URIGrammar.UserInfo.Parse(Consts.User2);
			Assert.AreEqual("j.doe", userInfo.User);
			Assert.AreEqual("secret", userInfo.Password);
			userInfo = URIGrammar.UserInfo.Parse(Consts.User3);
			Assert.AreEqual("+1-212-555-1212", userInfo.User);
			Assert.AreEqual("1234", userInfo.Password);
			userInfo = URIGrammar.UserInfo.Parse(Consts.User4);
			Assert.AreEqual("1212", userInfo.User);

			userInfo = URIGrammar.UserInfo.Parse("j.doe@");
			Assert.AreEqual("j.doe", userInfo.User);
		}
		[TestMethod]
		public void ShouldParseHostPort()
		{
			HostPort hostPort;

			hostPort = URIGrammar.HostPort.Parse(Consts.HostPort1);
			Assert.AreEqual("big.com", hostPort.Host);
			Assert.AreEqual(0, hostPort.Port);
			hostPort = URIGrammar.HostPort.Parse(Consts.HostPort2);
			Assert.AreEqual("big.com", hostPort.Host);
			Assert.AreEqual(1234, hostPort.Port);
			hostPort = URIGrammar.HostPort.Parse(Consts.HostPort3);
			Assert.AreEqual("10.1.2.3", hostPort.Host);
			Assert.AreEqual(0, hostPort.Port);
			hostPort = URIGrammar.HostPort.Parse(Consts.HostPort4);
			Assert.AreEqual("10.1.2.3", hostPort.Host);
			Assert.AreEqual(1234, hostPort.Port);
		}

		[TestMethod]
		public void ShouldParseURI1()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI1);
			Assert.AreEqual("j.doe", uri.UserInfo.User);
			Assert.AreEqual("big.com", uri.HostPort.Host);
		}

		[TestMethod]
		public void ShouldParseURI2()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI2);
			Assert.AreEqual("j.doe", uri.UserInfo.User);
			Assert.AreEqual("secret", uri.UserInfo.Password);
			Assert.AreEqual("big.com", uri.HostPort.Host);
			Assert.AreEqual(1, uri.Parameters.Length);
			Assert.AreEqual("transport", uri.Parameters[0].Name);
			Assert.AreEqual("tcp", uri.Parameters[0].Value);
		}

		[TestMethod]
		public void ShouldParseURI3()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI3);
			Assert.AreEqual("j.doe", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("big.com", uri.HostPort.Host);
			Assert.AreEqual(1, uri.Headers.Length);
			Assert.AreEqual("subject", uri.Headers[0].Name);
			Assert.AreEqual("project", uri.Headers[0].Value);
		}

		[TestMethod]
		public void ShouldParseURI4()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI4);
			Assert.AreEqual("+1-212-555-1212", uri.UserInfo.User);
			Assert.AreEqual("1234",uri.UserInfo.Password);
			Assert.AreEqual("gateway.com", uri.HostPort.Host);
			Assert.AreEqual(1, uri.Parameters.Length);
			Assert.AreEqual("user", uri.Parameters[0].Name);
			Assert.AreEqual("phone", uri.Parameters[0].Value);
		}
		[TestMethod]
		public void ShouldParseURI5()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI5);
			Assert.AreEqual("1212", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("gateway.com", uri.HostPort.Host);
			Assert.AreEqual(0, uri.Parameters.Length);
			Assert.AreEqual(0, uri.Headers.Length);
		}
		[TestMethod]
		public void ShouldParseURI6()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI6);
			Assert.AreEqual("alice", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("10.1.2.3", uri.HostPort.Host);
			Assert.AreEqual(0, uri.Parameters.Length);
			Assert.AreEqual(0, uri.Headers.Length);
		}
		[TestMethod]
		public void ShouldParseURI7()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI7);
			Assert.AreEqual("alice", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("example.com", uri.HostPort.Host);
			Assert.AreEqual(0, uri.Parameters.Length);
			Assert.AreEqual(0, uri.Headers.Length);
		}
		
		[TestMethod]
		public void ShouldParseURI8()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI8);
			Assert.AreEqual("alice@example.com", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("gateway.com", uri.HostPort.Host);
			Assert.AreEqual(0, uri.Parameters.Length);
			Assert.AreEqual(0, uri.Headers.Length);
		}
		[TestMethod]
		public void ShouldParseURI9()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI9);
			Assert.AreEqual("alice", uri.UserInfo.User);
			Assert.IsNull( uri.UserInfo.Password);
			Assert.AreEqual("registrar.com", uri.HostPort.Host);
			Assert.AreEqual(1, uri.Parameters.Length);
			Assert.AreEqual("method", uri.Parameters[0].Name);
			Assert.AreEqual("REGISTER", uri.Parameters[0].Value);
		}
		[TestMethod]
		public void ShouldParseURI10()
		{
			URI uri;

			uri = URIGrammar.RequestURI.Parse(Consts.URI10);
			Assert.IsNull(uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("big.com", uri.HostPort.Host);
		}



	}
}
