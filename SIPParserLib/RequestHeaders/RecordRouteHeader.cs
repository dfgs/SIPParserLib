﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class RecordRouteHeader:RequestHeader<string>
	{
		public override string Name => "Record-Route";
		public RecordRouteHeader(string Value):base(Value)
		{
		}

	}
}
