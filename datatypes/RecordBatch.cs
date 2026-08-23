namespace LukeQuery.Datatypes;

/// <summary>
/// A RecordBatch object groups multiple columns together with a single schema.
/// </summary>
public class RecordBatch(Schema schema, List<IColumnVector> columnVectors)
{
    public Schema Schema = schema;
    public List<IColumnVector> ColumnVectors = columnVectors;

    /// <summary>
    /// Returns the number of rows.
    /// </summary>
    /// <returns>int</returns>
    public int RowCount() => ColumnVectors[0].Size();

    /// <summary>
    /// Returns the number of columns.
    /// </summary>
    /// <returns>int</returns>
    public int ColumnCount() => ColumnVectors.Count;

    /// <summary>
    /// Gets the vector at the specified index.
    /// </summary>
    /// <param name="i">Index</param>
    /// <returns>IColumnVector</returns>
    public IColumnVector GetVector(int i) => ColumnVectors[i];

    /// <summary>
    /// Returns the contents of the record batch as a string in CSV format.
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        string str = "";

        foreach (Field field in schema.Fields)
        {
            str += field.Name + ",";
        }
        
        str += "\n";

        for (int i = 0; i <= RowCount()-1; i++)
        {
            for (int j = 0; j <= ColumnCount()-1; j++)
            {
                str += GetVector(j).GetValue(i) + ",";
            }
            str += "\n";
        }

        return str;
    }
}