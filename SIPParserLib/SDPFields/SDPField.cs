﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public abstract class SDPField
	{
		public abstract string Name { get; }
		public  abstract string DisplayValue { get; }
	}
}
