using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GenericParameterTableBuilder
{
	// For all this code you may want to use case statements since they are slightly faster than else if statements - Colton
	/* Builds a single column table using generic parameters passed in.
	 * Tests each parameter to figue out which primitive data type it is
	 * and creates a SQL statement with the value type that corresponds 
	 * to the data type of the generic parameter.
	 */
	public static string BuildGenericParameterTable<T>(string name, string column, IEnumerable<T> values)
	{
		var testType = values.First();
        string type = "";
        int counter = values.Count() - 1;
        StringBuilder sqlString = new StringBuilder();

		// Testing data type and setting type to corresponding SQL variable type.
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
			// Handling the special case where we want to surround string in single quotes in the table.
			var columnMax = values.Cast<string>().Aggregate((max, cur) => max.Length > cur.Length ? max : cur);
			sqlString.Append($"DECLARE @@{name} TABLE({column} VARCHAR({columnMax.Length}));");\
			foreach(var value in values)
			{
				if(counter == values.Count() -1 || counter % 1000 == 999)
				{
					sqlString.Append($"INSERT INTO @@{name} ({column}) VALUES ");
				}
                sqlString.Append($"('{value}'){(counter % 1000 == 0 ? ";" : ",")}");
				counter--;
            }
		}
		else
		{
			// Throwing exception when variable type is not of an expected type.
			throw new ArgumentException($"Variable type is not currently supported: {testType}")
		}

		sqlString.Append($"DECLARE @@{name} TABLE({column} {type});");

        foreach (var value in values)
        {
            if (counter == columnValues.Count() - 1 || counter % 1000 == 999)
            {
                sqlString.Append($"INSERT INTO @@{name} ({column}) VALUES ");
            }
            sqlString.Append($"({value}){(counter % 1000 == 0 ? ";" : ",")}");
            counter--;
        }

        return sqlString;
    }

    /* Builds a double column table using generic parameters passed in.
	 * Tests each parameter to figue out which primitive data type it is
	 * and creates a SQL statement with the value type that corresponds 
	 * to the data type of the generic parameter.
	 */
    public static string BuildGenericParameterTable<T>(string name, string columnOne, string columnTwo, IEnumerable<T> valuesOne, IEnumerable<T> valuesTwo)
	{
        var testTypeOne = valuesOne.First();
        var testTypeTwo = valuesTwo.First();
        string typeOne = "";
        string typeTwo = "";
        int counter = columnOneValues.Count() - 1;


        // Testing data type of the first column and setting typeOne to corresponding SQL variable type.
        if (testTypeOne is bool)
        {
            typeOne = "BIT";
        }
        else if (testTypeOne is int)
        {
            typeOne = "INT";
        }
        else if (testTypeOne is long)
        {
            typeOne = "BIGINT";
        }
        else if (testTypeOne is float || testTypeOne is double)
        {
            typeOne = "DECIMAL";
        }
        else if (testTypeOne is string)
        {
            var columnMax = values.Cast<string>().Aggregate((max, cur) => max.Length > cur.Length ? max : cur);
            typeOne = $"VARCHAR({columnMax.Length})";
        }
        else
        {
            // Throwing exception when variable type is not of an expected type.
            throw new ArgumentException($"Variable type is not currently supported: {testType}")

        }

        // Testing data type of the second column and setting typeTwo to corresponding SQL variable type.
        if (testTypeTwo is bool)
        {
            typeTwo = "BIT";
        }
        else if (testTypeTwo is int)
        {
            typeTwo = "INT";
        }
        else if (testTypeTwo is long)
        {
            typeTwo = "BIGINT";
        }
        else if (testTypeTwo is float || testTypeTwo is double)
        {
            typeTwo = "DECIMAL";
        }
        else if (testTypeTwo is string)
        {
            var columnMax = values.Cast<string>().Aggregate((max, cur) => max.Length > cur.Length ? max : cur);
            typeOne = $"VARCHAR({columnMax.Length})";

        }
        else
        {
            // Throwing exception when variable type is not of an expected type.
            throw new ArgumentException($"Variable type is not currently supported: {testType}")
        }

        if(typeOne = string || typeTwo = string)
        {
            if (typeOne = string && !(typeTwo = string)) 
            {
                DoubleColumnWhereColumnOneString(name, columnOne, columnTwo, typeOne, typeTwo, valuesOne, valuesTwo);
            }
            else if (!(typeOne = string) && typeTwo = string)
            {
                DoubleColumnWhereColumnTwoString(name, columnOne, columnTwo, typeOne, typeTwo, valuesOne, valuesTwo);
            }
            else if (typeOne = string && typeTwo = string)
            {
                DoubleColumnWhereColumnOneTwoString(name, columnOne, columnTwo, typeOne, typeTwo, valuesOne, valuesTwo);
            }
        }

        sqlString.Append($"DECLARE @@{name} TABLE({columnOne} {typeOne}, {columnTwo} {typeTwo}));");

        foreach (var valuez in columnOne.Zip(columnTwo, (a, b) => new { a, b }))
        {
            if (counter == valuesOne.Count() - 1 || counter % 1000 == 999)
            {
                sqlString.Append($"INSERT INTO @@{name} ({columnOne}, {ColumnTwo}) VALUES ");
            }
            sqlString.Append($"({valuez.a}, {valuez.b}){(counter % 1000 == 0 ? ";" : ",")}");
            counter--;
        }

        return sqlString;
    }
	// probably should leave a comment here to explain this - Colton
    private static string DoubleColumnWhereColumnOneString<T>(string name, string columnOne, string columnTwo, string typeOne, string typeTwo IEnumerable<T> valuesOne, IEnumerable<T> valuesTwo)
    {
        int counter = columnOneValues.Count() - 1;

        sqlString.Append($"DECLARE @@{name} TABLE({columnOne} {typeOne}, {columnTwo} {typeTwo}));");

        foreach (var valuez in columnOne.Zip(columnTwo, (a, b) => new { a, b }))
        {
            if (counter == valuesOne.Count() - 1 || counter % 1000 == 999)
            {
                sqlString.Append($"INSERT INTO @@{name} ({columnOne}, {columnTwo}) VALUES ");
            }
            sqlString.Append($"('{valuez.a}', {valuez.b}){(counter % 1000 == 0 ? ";" : ",")}");
            counter--;
        }

        return sqlString;
    }
	// explain exactly what this is doing - Colton
    private static string DoubleColumnWhereColumnTwoString<T>(string name, string columnOne, string columnTwo, string typeOne, string typeTwo IEnumerable<T> valuesOne, IEnumerable<T> valuesTwo)
    {
        int counter = columnOneValues.Count() - 1;

        sqlString.Append($"DECLARE @@{name} TABLE({columnOne} {typeOne}, {columnTwo} {typeTwo}));");

        foreach (var valuez in columnOne.Zip(columnTwo, (a, b) => new { a, b }))
        {
            if (counter == valuesOne.Count() - 1 || counter % 1000 == 999)
            {
                sqlString.Append($"INSERT INTO @@{name} ({columnOne}, {columnTwo}) VALUES ");
            }
            sqlString.Append($"({valuez.a}, '{valuez.b}'){(counter % 1000 == 0 ? ";" : ",")}");
            counter--;
        }

        return sqlString;
    }

    private static string DoubleColumnWhereColumnOneTwoString<T>(string name, string columnOne, string columnTwo, string typeOne, string typeTwo IEnumerable<T> valuesOne, IEnumerable<T> valuesTwo)
    {
        int counter = columnOneValues.Count() - 1;

        sqlString.Append($"DECLARE @@{name} TABLE({columnOne} {typeOne}, {columnTwo} {typeTwo}));");

        foreach (var valuez in columnOne.Zip(columnTwo, (a, b) => new { a, b }))
        {
            if (counter == valuesOne.Count() - 1 || counter % 1000 == 999)
            {
                sqlString.Append($"INSERT INTO @@{name} ({columnOne}, {columnTwo}) VALUES ");
            }
            sqlString.Append($"('{valuez.a}', '{valuez.b}'){(counter % 1000 == 0 ? ";" : ",")}");
            counter--;
        }

        return sqlString;
    }
}
