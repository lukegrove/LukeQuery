using LukeQuery.Datatypes;
using LukeQuery.DataSource;
using LukeQuery.LogicalPlan;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello from LukeQuery!");
        
        string path = "/Users/lukegrove/Desktop/LukeQueryCSV.txt";
        CsvDataSource ds = new(path, false, 4);

        /*IEnumerable<RecordBatch> recordBatches = ds.Scan([]);//["id", "name", "department", "salary"]);
        
        foreach (RecordBatch rb in recordBatches)
        {
            Console.WriteLine("==>> NEW BATCH");
            Console.WriteLine($"Record batch has {rb.RowCount()} rows and {rb.ColumnCount()} columns.");
            Console.WriteLine($"{rb.ToCSV()}");
        }*/

        //SELECT name, salary * 1.1 AS new_salary FROM employees WHERE department = 'Engineering'

        Scan scan = new(path, ds, []);
        Console.WriteLine(scan.ToString());

        LiteralString engineering = new("Engineering");
        Column column = new Column("department");
        Eq eq = new Eq(column, engineering);
        Selection filter = new(scan, eq);
        Console.WriteLine(filter.ToString());

        LiteralDouble salary = new(1.1);
        List<ILogicalExpr> projectionExprs = new() // C# collection initializer
        {
            new Column("name"),
            new Alias(new Multiply(new Column("salary"), salary), "new_salary")
        };
        Projection projection = new(filter, projectionExprs);
        Console.WriteLine(projection.ToString());
    }
}