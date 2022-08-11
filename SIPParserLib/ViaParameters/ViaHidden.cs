using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class ViaHidden : ViaParameter<string>
	{
		public override string Name => "hidden";
		public ViaHidden() : base("")
		{
		}

	}
}
