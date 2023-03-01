using NUnit.Framework.Internal;
using System.Text;
using GenericParameterTableBuilder;

namespace GenericParameterTableBuilder
{
    [TestClass]
    public class GenericParameterTableBuilderTests
    {
        [TestMethod]
        public void SingleColumnTableBuildsWhenTypeIsBool()
	{
		List<bool> values = new List<bool>();
		StringBuilder goal = new StringBuilder();

		for(int x = 0; x < 5; x++)
		{
			values.Add(true);
			values.Add(false);
		}

		goal.Append("DECLARE @@Name TABLE (Column BIT);");
        goal.Append("INSERT INTO @@Name TABLE VALUES ;");

		foreach(var value in values)
		{
			goal.Append(@"({value}),");
		}

		goal.Remove(goal.Length - 1, 1);
		goal.Append(";");

		var test = BuildGenericParameterTable("Name", "Column", values);

		Assert.Equals(goal.ToString(), test);
    }
    }
}