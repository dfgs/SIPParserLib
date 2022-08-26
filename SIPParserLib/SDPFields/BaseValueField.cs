using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public abstract class BaseValueField<T> : SDPField
	{

		public T Value
		{
			get;
			private set;
		}

		public override string DisplayValue => Value?.ToString()??"";

		public BaseValueField(T Value)
		{
			this.Value = Value;
		}


	}
}
