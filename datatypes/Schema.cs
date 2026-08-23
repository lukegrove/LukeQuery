namespace LukeQuery.Datatypes;

/// <summary>
/// A Schema object is the structure of a dataset, represented as a list of Field objects.
/// </summary>
public class Schema(List<Field> fields)
{
    public List<Field> Fields = fields;

    /// <summary>
    /// Returns the number of fields in the schema.
    /// </summary>
    /// <returns>int</returns>
    public int FieldCount() => Fields.Count;

    /// <summary>
    /// Selects a subset of the schema based on the provided field names.
    /// </summary>
    /// <param name="names">List of fields to select.</param>
    /// <returns>Schema</returns>
    public Schema Select(List<string> names)
    {
        List<Field> fields = [];

        foreach (string name in names)
        {
            foreach (Field field in Fields)
            {
                if (field.Name == name)
                {      
                    fields.Add(field);
                }
            }
        }

        return new Schema(fields);
    }

}