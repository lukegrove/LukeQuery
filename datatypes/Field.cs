using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// A Field object is a single column in a dataset, including its name, data type, and nullability.
/// </summary>
public class Field(string name, ArrowType type, bool? nullable = false)
{
    public string Name { get; } = name;
    public ArrowType Type { get; } = type;
    public bool? Nullable { get; } = nullable;
}