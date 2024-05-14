using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogLib;
using ModuleLib;

namespace SIPParserLib.Parsers
{
	public abstract class ClassStringParser<T> : Parser<T>,IClassStringParser<T>
		where T:class
	{
		public ClassStringParser(ILogger Logger) : base(Logger)
		{
		}

		protected abstract IEnumerable<Regex> OnGetRegexes();

		protected abstract bool OnParse(Regex Regex, Match Match,out T? Value);

		public bool Parse(Group? Group, out T? Value, bool IsMandatory)
		{
			LogEnter();

			Value = null;

			if (Group == null)
			{
				if (IsMandatory)
				{
					Log(LogLevels.Error, $"Failed to parse line, invalid format (null group)");
					return false;
				}
				else
				{
					return true;
				}
			}
			if (!Group.Success)
			{
				if (IsMandatory)
				{
					Log(LogLevels.Error, $"Failed to parse line, invalid format ({Group.Value})");
					return false;
				}
				else
				{
					return true;
				}
			}

			return Parse(Group.Value,out Value,IsMandatory);

		}
		public bool Parse(string? Line, out T? Value, bool IsMandatory)
		{
			Match match;

			LogEnter();

			Value = null;

			if (Line==null)
			{
				if (IsMandatory)
				{
					Log(LogLevels.Error, $"Failed to parse line, invalid format (null line)");
					return false;
				}
				else
				{
					return true;
				}
			}

			foreach (Regex regex in OnGetRegexes())
			{
				match = regex.Match(Line);
				if (match.Success) return OnParse(regex, match, out Value);
			}

			if (IsMandatory)
			{
				Log(LogLevels.Error, $"Failed to parse line, invalid format ({Line})");
				return false;
			}
			return true;
		}
		public bool ParseAll(Group? Group, char Separator, out T[]? Value, bool IsMandatory)
		{
			LogEnter();
			Value = null;

			if (Group == null)
			{
				if (IsMandatory)
				{
					Log(LogLevels.Error, $"Failed to parse line, invalid format (null group)");
					return false;
				}
				else
				{
					return true;
				}
			}
			if (!Group.Success)
			{
				if (IsMandatory)
				{
					Log(LogLevels.Error, $"Failed to parse line, invalid format ({Group.Value})");
					return false;
				}
				else
				{
					return true;
				}
			}
			
			return ParseAll(Group.Value,Separator, out Value, IsMandatory);

		}

		public bool ParseAll(string? Line, char Separator, out T[]? Value, bool IsMandatory)
		{
			Match match;
			string[] parts;
			bool result;
			T? partValue;
			List<T> items;

			LogEnter();

			Value = null;

			if (Line == null)
			{
				if (IsMandatory)
				{
					Log(LogLevels.Error, $"Failed to parse line, invalid format (null line)");
					return false;
				}
				else
				{
					return true;
				}
			}

			items = new List<T>();

			parts = Line.Split(Separator);
			foreach (string part in parts)
			{
				partValue = null;
				foreach (Regex regex in OnGetRegexes())
				{
					match = regex.Match(part);
					if (match.Success)
					{
						result = OnParse(regex, match, out partValue);
						if (!result)
						{
							Log(LogLevels.Error, $"Failed to parse part, invalid format ({part})");
							return false;
						}
						break;
					}
				}
				if (partValue != null)
				{
					items.Add(partValue);
					continue;
				}
				/*if (IsMandatory)
				{
					Log(LogLevels.Error, $"Failed to parse line, invalid format ({Line})");
					return false;
				}
				else return true;*/    // To define. Should we return true ?
				Log(LogLevels.Error, $"Failed to parse part, invalid format ({part})");
				return false;
			}
			Value = items.ToArray();
			return true;
		}


	}



}
