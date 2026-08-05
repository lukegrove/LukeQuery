using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

public class LiteralValueVector : ColumnVector
{
    public ArrowType ArrowType { get; }
    public double Value { get; }
    public int Size { get; }

    public LiteralValueVector(ArrowType arrowType, double value, int size)
    {
        ArrowType = arrowType;
        Value = value;
        Size = size;
    }

    public ArrowType getType()
    {
        return ArrowType;
    }

    public ArrowType getValue(int i)
    {
        if (i < 0 || i >= Size)
        {
            throw new IndexOutOfRangeException($"Index {i} is out of range for LiteralValueVector of size {Size}.");
        }
        else
        {
            return ArrowType;
        }
    }

    public int size()
    {
        return Size;
    }
}