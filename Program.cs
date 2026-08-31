using LukeQuery.Datatypes;
using LukeQuery.DataSource;
using LukeQuery.LogicalPlan;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello from LukeQuery!");
        
        string path = "/Users/lukegrove/Desktop/LukeQueryCSV.txt";

        //SELECT name, salary * 1.1 AS new_salary FROM employees WHERE department = 'Engineering'

        var df = LukeQuery.Execution.ExecutionContext.CSV(path, true, 4)
        .Filter(new Eq(new Column("department"), new LiteralString("Engineering")))
        .Project(new List<ILogicalExpr>
        {
            new Column("name"),
            new Alias(new Multiply(new Column("salary"), new LiteralDouble(1.1)), "new_salary")
        });

        Console.WriteLine(df.LogicalPlan().Pretty());
    }
}