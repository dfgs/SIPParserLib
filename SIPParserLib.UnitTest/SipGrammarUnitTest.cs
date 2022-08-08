﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SipGrammarUnitTest
	{
		

		[TestMethod]
		public void ShouldParseHeaderValue()
		{
			string result;

			result = SIPGrammar.HeaderValue.Parse("OmniPCX Enterprise R12.1 m2.300.23\r\n", ' ');
			Assert.AreEqual("OmniPCX Enterprise R12.1 m2.300.23", result);
		}
		[TestMethod]
		public void ShouldParseHeader()
		{
			MessageHeader<string> result;

			result = (MessageHeader<string>)SIPGrammar.UserAgentHeader.Parse("User-Agent: OmniPCX Enterprise R12.1 m2.300.23\r\n", ' ');
			Assert.AreEqual("User-Agent", result.Name);
			Assert.AreEqual("OmniPCX Enterprise R12.1 m2.300.23", result.Value);

			result = (MessageHeader<string>)SIPGrammar.AllowHeader.Parse("Allow: INVITE, ACK, CANCEL, BYE, PRACK, NOTIFY, SUBSCRIBE, OPTIONS, UPDATE, INFO\r\n", ' ');
			Assert.AreEqual("Allow", result.Name);
			Assert.AreEqual("INVITE, ACK, CANCEL, BYE, PRACK, NOTIFY, SUBSCRIBE, OPTIONS, UPDATE, INFO", result.Value);
		}

		[TestMethod]
		public void ShouldParseHeaderLine1()
		{
			MessageHeader[] result;

			result = SIPGrammar.MessageHeaders.Parse(Consts.HeaderLine1, ' ').ToArray();
			Assert.AreEqual(13, result.Length);
			Assert.IsTrue(result[0] is ViaHeader);
			Assert.IsTrue(result[1] is AllowHeader);
		}

		[TestMethod]
		public void ShouldParseHeaderLine2()
		{
			MessageHeader[] result;

			result = SIPGrammar.MessageHeaders.Parse(Consts.HeaderLine2, ' ').ToArray();
			Assert.AreEqual(15, result.Length);
			Assert.IsTrue(result[0] is AllowHeader);
			Assert.IsTrue(result[2] is UserAgentHeader);
		}
		[TestMethod]
		public void ShouldParseHeaderLine3()
		{
			MessageHeader[] result;

			result = SIPGrammar.MessageHeaders.Parse(Consts.HeaderLine3, ' ').ToArray();
			Assert.AreEqual(12, result.Length);
			Assert.IsTrue(result[0] is ToHeader);
			Assert.IsTrue(result[1] is FromHeader);
			Assert.IsTrue(result[2] is CallIDHeader);
		}


		[TestMethod]
		public void ShouldParseRequestLine1()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine1, ' ');
			Assert.AreEqual("INVITE", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("ecb.core.nord:5060", result.RequestURI.HostPort.ToString());
			Assert.AreEqual("sip:+33140143960@ecb.core.nord:5060;user=phone", result.RequestURI.ToString());
		}
		[TestMethod]
		public void ShouldParseRequestLine2()
		{
			RequestLine result;

			result = SIPGrammar.RequestLine.Parse(Consts.RequestLine2, ' ');
			Assert.AreEqual("INVITE", result.Method);
			Assert.AreEqual("SIP/2.0", result.SIPVersion);
			Assert.AreEqual("100.127.1.1", result.RequestURI.HostPort.ToString());
			Assert.AreEqual("sip:0243444265@100.127.1.1;transport=UDP;user=phone", result.RequestURI.ToString());
		}

		[TestMethod]
		public void ShouldParseStatusLine3()
		{
			StatusLine result;

			result = SIPGrammar.StatusLine.Parse(Consts.StatusLine3, ' ');
			Assert.AreEqual("180", result.StatusCode);
			Assert.AreEqual("Ringing", result.Reason);
		}


		[TestMethod]
		public void ShouldParseInvite1()
		{
			Request message;

			message=(Request)SIPGrammar.SIPMessage.Parse(Consts.Invite1,' ');
			Assert.AreEqual(13, message.Headers.Length);
			Assert.AreEqual("INVITE", message.RequestLine.Method);
			Assert.AreEqual("+33140143960", message.RequestLine.RequestURI.UserInfo.User);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}



		
		
		[TestMethod]
		public void ShouldParseInvite2()
		{
			Request message;

			message = (Request)SIPGrammar.SIPMessage.Parse(Consts.Invite2, ' ');
			Assert.AreEqual(15, message.Headers.Length);
			Assert.AreEqual("INVITE", message.RequestLine.Method);
			Assert.AreEqual("0243444265", message.RequestLine.RequestURI.UserInfo.User);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}


		[TestMethod]
		public void ShouldParseResponse1()
		{
			Response message;

			message = (Response)SIPGrammar.SIPMessage.Parse(Consts.Ringing1, ' ');
			Assert.AreEqual("180", message.StatusLine.StatusCode);
			Assert.AreEqual("Ringing", message.StatusLine.Reason);
			Assert.AreEqual(12, message.Headers.Length);
			Assert.IsTrue(string.IsNullOrEmpty(message.Body));
		}
		[TestMethod]
		public void ShouldParseResponse2()
		{
			Response message;

			message = (Response)SIPGrammar.SIPMessage.Parse(Consts.OK1, ' ');
			Assert.AreEqual("200", message.StatusLine.StatusCode);
			Assert.AreEqual("OK", message.StatusLine.Reason);
			Assert.AreEqual(13, message.Headers.Length);
			Assert.IsFalse(string.IsNullOrEmpty(message.Body));
		}



	}
}
