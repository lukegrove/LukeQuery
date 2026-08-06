using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// A Field object is a single column in a dataset, including its name, data type, and nullability.
/// </summary>
public class Field(string name, ArrowType type, int nullable)
{
    public string Name { get; } = name;
    public ArrowType Type { get; } = type;
    public int Nullable { get; } = nullable;
}