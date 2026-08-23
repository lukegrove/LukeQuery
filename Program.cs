using LukeQuery.Datatypes;
using LukeQuery.DataSource;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello from LukeQuery!");
        
        Field field1, field2, field3, field4;

        field1 = new("id", ArrowTypes.StringType, true);
        field2 = new("name", ArrowTypes.StringType, true);
        field3 = new("department", ArrowTypes.StringType, true);
        field4 = new("salary", ArrowTypes.DoubleType, true);

        List<Field> fields = [field1, field2, field3, field4];

        Schema schema = new(fields);

        /*Schema schemaScan = schema.Select(["name", "salary"]);
        Console.WriteLine(
        $"Schema has {schema.FieldCount()} fields: " +
        $"{string.Join(", ", schema.Fields.Select(field => field.Name))}");
        Console.WriteLine(
        $"I selected NAME and SALARY from the schema, finding {schemaScan.FieldCount()} fields:" +
        $" {string.Join(", ", schemaScan.Fields.Select(field => field.Name))}");*/

        ArrowFieldVector vector1, vector2, vector3;
        LiteralValueVector vector4;

        vector1 = new(field1, ["1", "2", "3"]);
        vector2 = new(field2, ["Alice", "Bob", "Carol"]);
        vector3 = new(field3, ["Engineering", "Sales", "Engineering"]);
        vector4 = new(ArrowTypes.StringType, "$100", 3);

        List<IColumnVector> columnVectors = [vector1, vector2, vector3, vector4];

        /*Console.WriteLine($"Column vectors:\n");
        foreach(var vector in columnVectors) {
            Console.WriteLine(vector);
        }*/

        RecordBatch recordBatch = new(schema, columnVectors);

        //Console.WriteLine($"Record batch has {recordBatch.RowCount()} rows and {recordBatch.ColumnCount()} columns.");
        //Console.WriteLine($"The value of field 4 at row 3 is {recordBatch.GetField(3).GetValue(2)}.");
        //Console.WriteLine($"{recordBatch.ToString()}");

        /*string docpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        using (StreamWriter outputFile = new(Path.Combine(docpath, "LukeQueryCSV.txt")))
        {
            outputFile.WriteLine(recordBatch.ToString());
        }*/

        CsvDataSource ds = new("/Users/lukegrove/Desktop/LukeQueryCSV.txt", false, 4);
        IEnumerable<RecordBatch> recordBatches = ds.Scan([]);//(["id", "name", "department"]);
        
        foreach (RecordBatch rb in recordBatches)
        {
            Console.WriteLine("==>> NEW BATCH");
            Console.WriteLine($"Record batch has {rb.RowCount()} rows and {rb.ColumnCount()} columns.");
            Console.WriteLine($"{rb.ToString()}");
        }
    }
}