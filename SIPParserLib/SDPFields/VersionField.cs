using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class VersionField : BaseValueField<byte>
	{
		public override char Name => 'v';


		public VersionField(byte Value):base(Value)
		{
		}


	}
}
