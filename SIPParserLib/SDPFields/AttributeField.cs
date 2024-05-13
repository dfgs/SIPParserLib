using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class AttributeField : BaseValueField<SDPAttribute>
	{
		public override string Name => "a";


		public AttributeField(SDPAttribute Value):base(Value)
		{
		}


	}
}
