using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GenericTableBuilder
{
	public static string BuildTable<T>(string name, string column, IEnumerable<T> values)
	{
		var testType = values.First();
		StringBuilder declare = new StringBuilder();
		int counter;
		string type = "";

		if(type is bool)
		{

		}
		else if(type is int)
		{

		}
		else if(type is long)
		{

		}
		else if(type is float || type is double)
		{

		}

	}
}
