using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ConnectionField : SDPField
	{
		public override string Name => "c";


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

		public override string DisplayValue => $"{NetworkType} {AddressType} {Address}";


		public ConnectionField(string NetworkType,string AddressType,string Address)
		{
			this.NetworkType = NetworkType;this.AddressType = AddressType;this.Address = Address;
		}


	}
}
