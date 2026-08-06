using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// A Schema object is the structure of a dataset, represented as a list of Field objects.
/// </summary>
public class Schema(List<Field> fields)
{
    public List<Field> Fields = fields;

    public int FieldCount() => Fields.Count;

}