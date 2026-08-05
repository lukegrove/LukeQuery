using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

public interface ColumnVector
{
    public ArrowType getType()
    {
        throw new NotImplementedException();
    }

    public ArrowType getValue(int i)
    {
        throw new NotImplementedException();
    }

    public int size()
    {
        throw new NotImplementedException();
    }
}