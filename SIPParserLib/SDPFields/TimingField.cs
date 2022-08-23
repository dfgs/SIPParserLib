﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public class TimingField : SDPField
	{
		public override char Name => 't';

		public uint StartTime
		{
			get;
			private set;
		}
		public uint StopTime
		{
			get;
			private set;
		}

		public TimingField(uint StartTime,uint StopTime)
		{
			this.StartTime = StartTime;this.StopTime = StopTime;
		}


	}
}
