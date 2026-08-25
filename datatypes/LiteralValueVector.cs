using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// A LiteralValueVector is a column vector that contains a single literal value repeated for by the size.
/// </summary>
/// <param name="arrowType">ArrowType</param>
/// <param name="value">double</param>
/// <param name="size">int</param>
public class LiteralValueVector<T>(ArrowType arrowType, T value, int size) : IColumnVector
{
    public ArrowType ArrowType { get; } = arrowType;
    public T Value { get; } = value;
    public int VectorSize { get; } = size;

    /// <summary>
    /// Returns the vector type.
    /// </summary>
    /// <returns>ArrowType</returns>
    public new ArrowType GetType()
    {
        return ArrowType;
    }

    /// <summary>
    /// Gets the value of the vector at the index.
    /// </summary>
    /// <param name="i">Index</param>
    /// <returns>string</returns>
    public object GetValue(int i)
    {
        if (i < 0 || i >= VectorSize)
        {
            throw new IndexOutOfRangeException($"Index {i} is out of range for LiteralValueVector of size {Size}.");
        }
        else
        {
            return Value!;
        }
    }

    /// <summary>
    /// Returns the size of the vector.
    /// </summary>
    /// <returns>int</returns>
    public int Size()
    {
        return VectorSize;
    }
}