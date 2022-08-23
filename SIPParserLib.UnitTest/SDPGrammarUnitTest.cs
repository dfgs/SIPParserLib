using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SDPGrammarUnitTest
	{
		

		[TestMethod]
		public void ShouldParseField()
		{
			OriginField? o;

			o = SDPGrammar.SDPField.Parse("o=- 616920219 1669365097 IN IP4 185.221.89.20\r\n") as OriginField;
			Assert.IsNotNull(o);
			Assert.AreEqual('o', o.Name);
			Assert.AreEqual("-", o.UserName);
			Assert.AreEqual("616920219", o.SessionID);
			Assert.AreEqual("1669365097", o.SessionVersion);
			Assert.AreEqual("IN", o.NetworkType);
			Assert.AreEqual("IP4", o.AddressType);
			Assert.AreEqual("185.221.89.20", o.Address);
		}
		[TestMethod]
		public void ShouldParseAttribute()
		{
			AttributeField? attributeField;

			attributeField = SDPGrammar.AttributeField.Parse("a=rtpmap:8 PCMA/8000\r\n");
			Assert.IsNotNull(attributeField);
			Assert.AreEqual('a', attributeField.Name);
			Assert.AreEqual("rtpmap", attributeField.Value.Name);
			Assert.AreEqual("8 PCMA/8000", attributeField.Value.Value);

			attributeField = SDPGrammar.AttributeField.Parse("a=test\r\n");
			Assert.IsNotNull(attributeField);
			Assert.AreEqual('a', attributeField.Name);
			Assert.AreEqual("test", attributeField.Value.Name);
			Assert.IsNull(attributeField.Value.Value);
		}

		[TestMethod]
		public void ShouldParseSDP1()
		{
			SDP? result;
			VersionField? v;
			OriginField? o;
			SessionNameField? s;
			ConnectionField? c;
			TimingField? t;
			MediaField? m;
			AttributeField? a;
			AttributeField[] a2;


			result = SDPGrammar.SDP.Parse(Consts.SDP1);
			Assert.IsNotNull(result);
			Assert.AreEqual(10,result.Count);

			v = result.GetField<VersionField>();
			Assert.IsNotNull(v);
			Assert.AreEqual('v', v.Name);
			Assert.AreEqual((byte)0, v.Value);

			o = result.GetField<OriginField>();
			Assert.IsNotNull(o);
			Assert.AreEqual('o', o.Name);
			Assert.AreEqual("-", o.UserName);
			Assert.AreEqual("616920219", o.SessionID);
			Assert.AreEqual("1669365097", o.SessionVersion);
			Assert.AreEqual("IN", o.NetworkType);
			Assert.AreEqual("IP4", o.AddressType);
			Assert.AreEqual("185.221.89.20", o.Address);

			s = result.GetField<SessionNameField>();
			Assert.IsNotNull(s);
			Assert.AreEqual('s', s.Name);
			Assert.AreEqual("session", s.Value);

			c = result.GetField<ConnectionField>();
			Assert.IsNotNull(c);
			Assert.AreEqual('c', c.Name);
			Assert.AreEqual("IN", o.NetworkType);
			Assert.AreEqual("IP4", o.AddressType);
			Assert.AreEqual("185.221.89.20", o.Address);

			t = result.GetField<TimingField>();
			Assert.IsNotNull(t);
			Assert.AreEqual('t', t.Name);
			Assert.AreEqual((uint)0, t.StartTime);
			Assert.AreEqual((uint)0, t.StopTime);

			m = result.GetField<MediaField>();
			Assert.IsNotNull(m);
			Assert.AreEqual('m', m.Name);
			Assert.AreEqual("audio", m.Media);
			Assert.AreEqual((ushort)21606, m.Port);
			Assert.AreEqual("RTP/AVP", m.Protocol);
			Assert.AreEqual("8 101", m.Format);

			a=result.GetField<AttributeField>();
			Assert.IsNotNull(a);
			Assert.AreEqual('a', a.Name);
			Assert.AreEqual("rtpmap", a.Value.Name);
			Assert.AreEqual("8 PCMA/8000", a.Value.Value);

			a2 = result.GetFields<AttributeField>().ToArray();
			Assert.AreEqual(4, a2.Length);
		}




	}
}
