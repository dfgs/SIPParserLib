using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLib;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class URIGrammarUnitTest
	{
		[TestMethod]
		public void ShouldParseURLParameter()
		{
			URLParameter parameter;

			parameter = URIGrammar.URLParameter.Parse("transport=UDP");
			Assert.AreEqual("transport", parameter.Name);
			Assert.AreEqual("UDP", parameter.Value);
			parameter = URIGrammar.URLParameter.Parse("user=phone");
			Assert.AreEqual("user", parameter.Name);
			Assert.AreEqual("phone", parameter.Value);
			parameter = URIGrammar.URLParameter.Parse("user=phone");
			Assert.AreEqual("user", parameter.Name);
			Assert.AreEqual("phone", parameter.Value);
			parameter = URIGrammar.URLParameter.Parse("sdp_iwf");
			Assert.AreEqual("sdp_iwf", parameter.Name);
			Assert.AreEqual("", parameter.Value);
		}
		[TestMethod]
		public void ShouldParseURLParameters()
		{
			URLParameter[] parameters;

			parameters = URIGrammar.URLParameters.Parse(";transport=UDP;user=phone").ToArray();
			Assert.AreEqual(2, parameters.Length);
			Assert.AreEqual("transport", parameters[0].Name);
			Assert.AreEqual("UDP", parameters[0].Value);
			Assert.AreEqual("user", parameters[1].Name);
			Assert.AreEqual("phone", parameters[1].Value);

			parameters = URIGrammar.URLParameters.Parse(";user=phone;BP=1086996").ToArray();
			Assert.AreEqual(2, parameters.Length);
			Assert.AreEqual("user", parameters[0].Name);
			Assert.AreEqual("phone", parameters[0].Value);
			Assert.AreEqual("BP", parameters[1].Name);
			Assert.AreEqual("1086996", parameters[1].Value);



		}

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
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI1);
			Assert.AreEqual("j.doe", uri.UserInfo.User);
			Assert.AreEqual("big.com", uri.HostPort.Host);
			Assert.AreEqual("sip", uri.Scheme);
		}

		[TestMethod]
		public void ShouldParseURI2()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI2);
			Assert.AreEqual("j.doe", uri.UserInfo.User);
			Assert.AreEqual("secret", uri.UserInfo.Password);
			Assert.AreEqual("big.com", uri.HostPort.Host);
			Assert.AreEqual(1, uri.Parameters.Length);
			Assert.AreEqual("transport", uri.Parameters[0].Name);
			Assert.AreEqual("tcp", uri.Parameters[0].Value);
			Assert.AreEqual("sip", uri.Scheme);
		}

		[TestMethod]
		public void ShouldParseURI3()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI3);
			Assert.AreEqual("j.doe", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("big.com", uri.HostPort.Host);
			Assert.AreEqual(1, uri.Headers.Length);
			Assert.AreEqual("subject", uri.Headers[0].Name);
			Assert.AreEqual("project", uri.Headers[0].Value);
			Assert.AreEqual("sip", uri.Scheme);
		}

		[TestMethod]
		public void ShouldParseURI4()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI4);
			Assert.AreEqual("+1-212-555-1212", uri.UserInfo.User);
			Assert.AreEqual("1234",uri.UserInfo.Password);
			Assert.AreEqual("gateway.com", uri.HostPort.Host);
			Assert.AreEqual(1, uri.Parameters.Length);
			Assert.AreEqual("user", uri.Parameters[0].Name);
			Assert.AreEqual("phone", uri.Parameters[0].Value);
			Assert.AreEqual("sip", uri.Scheme);
		}
		[TestMethod]
		public void ShouldParseURI5()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI5);
			Assert.AreEqual("1212", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("gateway.com", uri.HostPort.Host);
			Assert.AreEqual(0, uri.Parameters.Length);
			Assert.AreEqual(0, uri.Headers.Length);
			Assert.AreEqual("sip", uri.Scheme);
		}
		[TestMethod]
		public void ShouldParseURI6()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI6);
			Assert.AreEqual("alice", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("10.1.2.3", uri.HostPort.Host);
			Assert.AreEqual(0, uri.Parameters.Length);
			Assert.AreEqual(0, uri.Headers.Length);
			Assert.AreEqual("sip", uri.Scheme);
		}
		[TestMethod]
		public void ShouldParseURI7()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI7);
			Assert.AreEqual("alice", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("example.com", uri.HostPort.Host);
			Assert.AreEqual(0, uri.Parameters.Length);
			Assert.AreEqual(0, uri.Headers.Length);
			Assert.AreEqual("sip", uri.Scheme);
		}

		[TestMethod]
		public void ShouldParseURI8()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI8);
			Assert.AreEqual("alice@example.com", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("gateway.com", uri.HostPort.Host);
			Assert.AreEqual(0, uri.Parameters.Length);
			Assert.AreEqual(0, uri.Headers.Length);
			Assert.AreEqual("sip", uri.Scheme);
		}
		[TestMethod]
		public void ShouldParseURI9()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI9);
			Assert.AreEqual("alice", uri.UserInfo.User);
			Assert.IsNull( uri.UserInfo.Password);
			Assert.AreEqual("registrar.com", uri.HostPort.Host);
			Assert.AreEqual(1, uri.Parameters.Length);
			Assert.AreEqual("method", uri.Parameters[0].Name);
			Assert.AreEqual("REGISTER", uri.Parameters[0].Value);
			Assert.AreEqual("sip", uri.Scheme);
		}
		[TestMethod]
		public void ShouldParseURI10()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI10);
			Assert.IsNull(uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("big.com", uri.HostPort.Host);
			Assert.AreEqual("sip", uri.Scheme);
		}
		[TestMethod]
		public void ShouldParseURI11()
		{
			SIPURL uri;

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI11);
			Assert.AreEqual("j.doe", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("big.com", uri.HostPort.Host);
			Assert.AreEqual(2, uri.Headers.Length);
			Assert.AreEqual("subject", uri.Headers[0].Name);
			Assert.AreEqual("project", uri.Headers[0].Value);
			Assert.AreEqual("name", uri.Headers[1].Name);
			Assert.AreEqual("test", uri.Headers[1].Value);
			Assert.AreEqual("sip", uri.Scheme);
		}
		[TestMethod]
		public void ShouldParseURI12()
		{
			SIPURL uri;

			//sip:0243444265@100.127.1.1;transport=UDP;user=phone

			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI12);
			Assert.AreEqual("0243444265", uri.UserInfo.User);
			Assert.IsNull(uri.UserInfo.Password);
			Assert.AreEqual("100.127.1.1", uri.HostPort.Host);
			Assert.AreEqual(2, uri.Parameters.Length);
			Assert.AreEqual("transport", uri.Parameters[0].Name);
			Assert.AreEqual("UDP", uri.Parameters[0].Value);
			Assert.AreEqual("user", uri.Parameters[1].Name);
			Assert.AreEqual("phone", uri.Parameters[1].Value);
			Assert.AreEqual("sip", uri.Scheme);
		}
		[TestMethod]
		public void ShouldParseURI13()
		{
			TELURL uri;


			uri = (TELURL)URIGrammar.URI.Parse(Consts.URI13);
			Assert.AreEqual("+334556677", uri.PhoneNumber);
			Assert.AreEqual("tel", uri.Scheme);

		}

		[TestMethod]
		public void ShouldParseURI14()
		{
			SIPURL uri;

		
			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI14);
			Assert.AreEqual("+33450050545", uri.UserInfo.User);
			Assert.AreEqual("sip", uri.Scheme);
			Assert.AreEqual(2, uri.Parameters.Length);
			Assert.AreEqual("user", uri.Parameters[0].Name);
			Assert.AreEqual("phone", uri.Parameters[0].Value);
			Assert.AreEqual("BP", uri.Parameters[1].Name);
			Assert.AreEqual("1086996", uri.Parameters[1].Value);

		}
		[TestMethod]
		public void ShouldParseURI15()
		{
			SIPURL uri;


			uri = (SIPURL)URIGrammar.URI.Parse(Consts.URI15);
			Assert.AreEqual(null, uri.UserInfo.User);
			Assert.AreEqual("sip", uri.Scheme);
			Assert.AreEqual(2, uri.Parameters.Length);
			Assert.AreEqual("transport", uri.Parameters[0].Name);
			Assert.AreEqual("udp", uri.Parameters[0].Value);
			Assert.AreEqual("sdp_iwf", uri.Parameters[1].Name);
			Assert.AreEqual("", uri.Parameters[1].Value);

		}


		[TestMethod]
		public void ShouldParseAddress1()
		{
			Address address;

			address = URIGrammar.Address.Parse(Consts.Address1,' ');
			Assert.AreEqual("A. G. Bell", address.DisplayName);
			Assert.AreEqual("agb", ((SIPURL)address.URI).UserInfo.User);
			Assert.AreEqual("bell-telephone.com", ((SIPURL)address.URI).HostPort.Host);
			Assert.AreEqual("", address.Tag);
			Assert.AreEqual(Consts.Address1, address.ToString());

			address = URIGrammar.Address.Parse(Consts.Address2, ' ');
			Assert.AreEqual("", address.DisplayName);
			Assert.AreEqual("+12125551212", ((SIPURL)address.URI).UserInfo.User);
			Assert.AreEqual("server.phone2net.com", ((SIPURL)address.URI).HostPort.Host);
			Assert.AreEqual("", address.Tag);
			Assert.AreEqual(Consts.Address2, address.ToString());

			address = URIGrammar.Address.Parse(Consts.Address3, ' ');
			Assert.AreEqual("Anonymous", address.DisplayName);
			Assert.AreEqual("c8oqz84zk7z", ((SIPURL)address.URI).UserInfo.User);
			Assert.AreEqual("privacy.org", ((SIPURL)address.URI).HostPort.Host);
			Assert.AreEqual("", address.Tag);
			Assert.AreEqual("\"Anonymous\" <sip:c8oqz84zk7z@privacy.org>", address.ToString());

			address = URIGrammar.Address.Parse(Consts.Address4, ' ');
			Assert.AreEqual("", address.DisplayName);
			Assert.AreEqual("+33663326291", ((SIPURL)address.URI).UserInfo.User);
			Assert.AreEqual("185.221.88.177", ((SIPURL)address.URI).HostPort.Host);
			Assert.AreEqual("user=phone", ((SIPURL)address.URI).Parameters[0].ToString());
			Assert.AreEqual("SDfefdf03-007302670000fdcf", address.Tag);
		}



	}
}
