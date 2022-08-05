using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class EncryptionHeader : RequestHeader<string>
	{
		public override string Name => "Encryption";
		public EncryptionHeader(string Value):base(Value)
		{
		}

	}
}
