using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPParserLib
{
	public abstract class ViaParameter
	{
		public abstract string Name
		{
			get;
		}
	}
	public abstract class ViaParameter<T>: ViaParameter
	{
		
		public T Value
		{
			get;
		}

		public ViaParameter(T Value)
		{
			this.Value = Value;
		}

		public override string ToString()
		{
			if (Value == null) return Name;
			if (Value.ToString() == "") return Name;			
			return $"{Name}: {Value}";
		}


	}


}
