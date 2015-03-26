using System;
using System.Data;

namespace AtApbdMpWf.Data
{
	public class ValuedSqlParameter
	{
		public SqlDbType Type { get; set; }
		public string ParameterName { get; set; }
		public Object Value { get; set; }

		public ValuedSqlParameter(SqlDbType type, string parameterName, object value)
		{
			Type = type;
			ParameterName = parameterName;
			Value = value;
		}
	}
}