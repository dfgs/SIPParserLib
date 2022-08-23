using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class OriginField : SDPField
	{
		public override char Name => 'o';

		public string UserName
		{
			get;
			private set;
		}
		public string SessionID
		{
			get;
			private set;
		}
		public string SessionVersion
		{
			get;
			private set;
		}
		public string NetworkType
		{
			get;
			private set;
		}
		public string AddressType
		{
			get;
			private set;
		}
		public string Address
		{
			get;
			private set;
		}

		

		public OriginField(string UserName,string SessionID,string SessionVersion,string NetworkType,string AddressType,string Address)
		{
			this.UserName = UserName;this.SessionID = SessionID;this.SessionVersion = SessionVersion;this.NetworkType = NetworkType;this.AddressType = AddressType;this.Address = Address;
		}


	}
}
