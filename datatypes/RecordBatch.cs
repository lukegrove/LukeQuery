using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

// Class should group multiple columns together with a schema
public class RecordBatch
{
    public Schema _schema;
    public List<ColumnVector> _fields;

    public RecordBatch(Schema schema, List<ColumnVector> fields)
    {
        _schema = schema;
        _fields = fields;
    }

    public int rowCount() => _fields.Count > 0 ? _fields[0].size() : 0;

    public int columnCount() => _fields.Count;

    public ColumnVector getField(int i) => _fields[i];

    // TODO: toCSV method
}