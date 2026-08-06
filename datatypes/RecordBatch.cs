using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// A RecordBatch object groups multiple columns together with a single schema.
/// </summary>
public class RecordBatch
{
    public Schema _schema;
    public List<IColumnVector> _columnVector;

    public RecordBatch(Schema schema, List<IColumnVector> columnVector)
    {
        _schema = schema;
        _columnVector = columnVector;
    }

    public int RowCount() => _columnVector[0].Size();

    public int ColumnCount() => _columnVector.Count;

    public IColumnVector GetField(int i) => _columnVector[i];

    // TODO: toCSV method
}