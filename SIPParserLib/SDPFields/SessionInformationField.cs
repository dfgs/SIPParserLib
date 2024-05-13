using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class SessionInformationField : BaseValueField<string>
	{
		public override string Name => "i";


		public SessionInformationField(string Value):base(Value)
		{
		}


	}
}
