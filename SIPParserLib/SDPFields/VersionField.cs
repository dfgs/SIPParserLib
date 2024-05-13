using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class VersionField : BaseValueField<byte>
	{
		public override string Name => "v";


		public VersionField(byte Value):base(Value)
		{
		}


	}
}
