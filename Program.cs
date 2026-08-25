using LukeQuery.Datatypes;
using LukeQuery.DataSource;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello from LukeQuery!");
        
        CsvDataSource ds = new("/Users/lukegrove/Desktop/LukeQueryCSV.txt", false, 4);
        IEnumerable<RecordBatch> recordBatches = ds.Scan([]);//["id", "name", "department", "salary"]);
        
        foreach (RecordBatch rb in recordBatches)
        {
            Console.WriteLine("==>> NEW BATCH");
            Console.WriteLine($"Record batch has {rb.RowCount()} rows and {rb.ColumnCount()} columns.");
            Console.WriteLine($"{rb.ToCSV()}");
        }
    }
}