using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class SessionNameField : BaseValueField<string>
	{
		public override char Name => 's';

	
		public SessionNameField(string Value):base(Value)
		{
		}


	}
}
