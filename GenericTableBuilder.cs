using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GenericTableBuilder
{
	public static string BuildTable<T>(string name, string column, IEnumerable<T> values)
	{
		var testType = values.First();
		StringBuilder sqlString = new StringBuilder();
		int counter;
		string type = "";

		if(testType is bool)
		{
			type = "BIT";
		}
		else if(testType is int)
		{
            type = "INT";
        }
		else if(testType is long)
		{
            type = "BIGINT";
        }
		else if(testType is float || testType is double)
		{
            type = "DECIMAL";
        }
		else if(testType is string)
		{
			// When variable type is string we want to surround value in single quotes.
			var columnMax = values.Cast<string>().Aggregate((max, cur) => max.Length > cur.Length ? max : cur);
			sqlString.Append($"DECLARE @@{name} TABLE({column} VARCHAR({columnMax.Length}));");\
			counter = values.Count() - 1;
			foreach(var value in values)
			{
				if(counter == columnValues.Count() -1 || counter % 1000 == 999)
				{
					sqlString.Append($"INSERT INTO @@{name} {column} VALUES ");
				}
                sqlString.Append($"('{value}'){(counter % 1000 == 0 ? ";" : ",")}");
				counter--;
            }
		}
		else
		{
			throw new ArgumentException($"Variable type is not currently supported: {testType}")
		}

		sqlString.Append($"DECLARE @@{name} TABLE({column} {type});");
		counter = values.Count() - 1;
        foreach (var value in values)
        {
            if (counter == columnValues.Count() - 1 || counter % 1000 == 999)
            {
                sqlString.Append($"INSERT INTO @@{name} {column} VALUES ");
            }
            sqlString.Append($"({value}){(counter % 1000 == 0 ? ";" : ",")}");
            counter--;
        }
    }
}
