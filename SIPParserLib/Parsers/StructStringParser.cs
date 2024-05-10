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
	public abstract class StructStringParser<T> : Parser<T>,IStructStringParser<T>
		where T:struct
	{
		public StructStringParser(ILogger Logger) : base(Logger)
		{
		}

		protected abstract Regex OnGetRegex();

		protected abstract bool OnParse(Match Match, out T? Value);

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

			return Parse(Group.Value, out Value, IsMandatory);

		}
		public bool Parse(string? Line, out T? Value, bool IsMandatory)
		{
			Match match;
			Regex regex;

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

			regex = OnGetRegex();
			match = regex.Match(Line);
			if (!match.Success)
			{
				if (IsMandatory)
				{
					Log(LogLevels.Error, $"Failed to parse line, invalid format ({Line})");
					return false;
				}
				else return true;
			}

			return OnParse(match, out Value);
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

			return ParseAll(Group.Value, Separator, out Value, IsMandatory);

		}

		public bool ParseAll(string? Line, char Separator, out T[]? Value, bool IsMandatory)
		{
			Match match;
			Regex regex;
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
			regex = OnGetRegex();
			parts = Line.Split(Separator);
			foreach (string part in parts)
			{
				match = regex.Match(part);
				if (!match.Success)
				{
					/*if (IsMandatory)
					{
						Log(LogLevels.Error, $"Failed to parse line, invalid format ({Line})");
						return false;
					}
					else return true;*/    // To define. Should we return true ?
					Log(LogLevels.Error, $"Failed to parse part, invalid format ({part})");
					return false;
				}

				result = OnParse(match, out partValue);
				if (!result)
				{
					Log(LogLevels.Error, $"Failed to parse part, invalid format ({part})");
					return false;
				}
				if (partValue != null) items.Add(partValue.Value);
			}
			Value = items.ToArray();
			return true;
		}







	}



}
