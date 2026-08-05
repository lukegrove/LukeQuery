using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

public class ArrowFieldVector : ColumnVector
{
    private readonly Field _field;
    private readonly List<Field> _fields = [];

    public ArrowFieldVector(Field field)
    {
        _field = field;
    }

    public ArrowType getType()
    {
        // Does this need to depend on a list?
        switch (_field.GetType().Name.ToLower())
        {
            case "bool":
                return ArrowTypes.BooleanType;
            case "int8":
                return ArrowTypes.Int8Type;
            case "int16":
                return ArrowTypes.Int16Type;
            case "int32":
                return ArrowTypes.Int32Type;
            case "int64":
                return ArrowTypes.Int64Type;
            case "uint8":
                return ArrowTypes.UInt8Type;
            case "uint16":
                return ArrowTypes.UInt16Type;
            case "uint32":
                return ArrowTypes.UInt32Type;
            case "uint64":
                return ArrowTypes.UInt64Type;
            case "float":
                return ArrowTypes.FloatType;
            case "double":
                return ArrowTypes.DoubleType;
            case "string":
                return ArrowTypes.StringType;
            case "binary":
                return ArrowTypes.BinaryType;
            case "date32":
                return ArrowTypes.DateDayType;
            case "interval_day_time":
                return ArrowTypes.IntervalDayTimeType;
            default:
                throw new NotImplementedException($"Arrow type for field '{_field}' is not implemented.");
        }
    }

    public ArrowType getValue(int i)
    {
        // Does this need to depend on a list?
        if (i >= 0)
        {
            return _fields[i].Type;
        }
        else
        {
            throw new IndexOutOfRangeException($"Index {i} is out of range for ArrowFieldVector.");
        }
    }

    public int size()
    {
        // Does this need to depend on a list?
        return _fields.Count;
    }
}