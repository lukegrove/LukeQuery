using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// Factory for creating Vectors based on Field type.
/// </summary>
/// <param name="field">Field</param>
public class VectorFactory(Field field)
{
    private readonly Field Field = field;

    /// <summary>
    /// Creates an ArrowFieldVector.
    /// </summary>
    /// <returns>IColumnVector</returns>
    public IColumnVector CreateFieldVector()
    {
        return Field.Type switch
        {
            BooleanType => new ArrowFieldVector<bool>(Field, []),
            Int8Type  => new ArrowFieldVector<sbyte>(Field, []),
            Int16Type => new ArrowFieldVector<short>(Field, []),
            Int32Type => new ArrowFieldVector<int>(Field, []),
            Int64Type => new ArrowFieldVector<long>(Field, []),
            UInt8Type => new ArrowFieldVector<byte>(Field, []),
            UInt16Type => new ArrowFieldVector<ushort>(Field, []),
            UInt32Type => new ArrowFieldVector<uint>(Field, []),
            UInt64Type => new ArrowFieldVector<ulong>(Field, []),
            FloatType => new ArrowFieldVector<float>(Field, []),
            DoubleType => new ArrowFieldVector<double>(Field, []),
            StringType => new ArrowFieldVector<string>(Field, []),
            BinaryType => new ArrowFieldVector<byte[]>(Field, []),
            Date32Type => new ArrowFieldVector<DateTime>(Field, []),
            IntervalType => new ArrowFieldVector<TimeSpan>(Field, []),
            _ => throw new NotImplementedException($"Arrow type for field type '{Field.Type}' is not implemented."),
        };
    }
}