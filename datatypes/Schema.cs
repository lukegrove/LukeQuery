using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

public class Schema
{
    public List<ColumnVector> _fields = new List<ColumnVector>();

    public Schema(List<ColumnVector> fields)
    {
        _fields = fields;
    }

    public int fieldCount() => _fields.Count;

}