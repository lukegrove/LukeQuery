using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// A ColumnVector is a single column of data in a dataset, defined by a Field object.
/// </summary>
public interface IColumnVector
{
    public ArrowType GetType()
    {
        throw new NotImplementedException();
    }

    public object GetValue(int i)
    {
        throw new NotImplementedException();
    }

    public void AddValue(object value)
    {
        throw new NotImplementedException();
    }

    public void SetValue(int i, object value)
    {
        throw new NotImplementedException();
    }

    public void SetNull(int i)
    {
        throw new NotImplementedException();
    }

    public int Size()
    {
        throw new NotImplementedException();
    }
}