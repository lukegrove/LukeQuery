using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// A LiteralValueVector is a column vector that contains a single literal value repeated for by the size.
/// </summary>
/// <param name="arrowType">ArrowType</param>
/// <param name="value">double</param>
/// <param name="size">int</param>
public class LiteralValueVector(ArrowType arrowType, double value, int size) : IColumnVector
{
    public ArrowType ArrowType { get; } = arrowType;
    public double Value { get; } = value;
    public int VectorSize { get; } = size;

    public new ArrowType GetType()
    {
        return ArrowType;
    }

    public ArrowType GetValue(int i)
    {
        if (i < 0 || i >= VectorSize)
        {
            throw new IndexOutOfRangeException($"Index {i} is out of range for LiteralValueVector of size {Size}.");
        }
        else
        {
            return ArrowType;
        }
    }

    public int Size()
    {
        return VectorSize;
    }
}