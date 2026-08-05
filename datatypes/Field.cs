using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

// Make a record?
public class Field : ColumnVector
{
    public string Name { get; }
    public ArrowType Type { get; }
    public int Nullable { get; }

    public Field(string name, ArrowType type, int nullable)
    {
        Name = name;
        Type = type;
        Nullable = nullable;
    }

    public ArrowType getType()
    {
        return Type;
    }

    public int size()
    {
        return 0;
    }
}