using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// An ArrowFieldVector is a single column of data in a dataset, defined by a Field object.
/// </summary>
/// <param name="field">Field</param>
public class ArrowFieldVector(Field field) : IColumnVector
{
    private readonly Field Field = field;

    public new ArrowType GetType()
    {
        return Field.GetType().Name.ToLower() switch
        {
            "bool"  => ArrowTypes.BooleanType,
            "int8"  => ArrowTypes.Int8Type,
            "int16" => ArrowTypes.Int16Type,
            "int32" => ArrowTypes.Int32Type,
            "int64" => ArrowTypes.Int64Type,
            "uint8" => ArrowTypes.UInt8Type,
            "uint16" => ArrowTypes.UInt16Type,
            "uint32" => ArrowTypes.UInt32Type,
            "uint64" => ArrowTypes.UInt64Type,
            "float" => ArrowTypes.FloatType,
            "double" => ArrowTypes.DoubleType,
            "string" => ArrowTypes.StringType,
            "binary" => ArrowTypes.BinaryType,
            "date32" => ArrowTypes.DateDayType,
            "interval_day_time" => ArrowTypes.IntervalDayTimeType,
            _ => throw new NotImplementedException($"Arrow type for field '{Field}' is not implemented."),
        };
    }

    /*public ArrowType getValue(int i)
    {
        if (i >= 0)
        {
            return _fields[i].Type;
        }
        else
        {
            throw new IndexOutOfRangeException($"Index {i} is out of range for ArrowFieldVector.");
        }
    }

    public int Size()
    {
        return _fields.Count;
    }*/
}