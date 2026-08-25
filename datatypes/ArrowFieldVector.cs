using Apache.Arrow.Types;
using LukeQuery.DataTypes;

namespace LukeQuery.Datatypes;

/// <summary>
/// An ArrowFieldVector is a single column of data in a dataset, defined by a Field object.
/// </summary>
/// <param name="field">Field</param>
public class ArrowFieldVector<T>(Field field, List<T> values) : IColumnVector
{
    private readonly Field Field = field;
    public List<T> Values = values;

    /// <summary>
    /// Returns the vector type.
    /// </summary>
    /// <returns>ArrowType</returns>
    public new ArrowType GetType()
    {
        return Field.Type;
    }

    /// <summary>
    /// Gets the value of the vector at the index.
    /// </summary>
    /// <param name="i">Index</param>
    /// <returns>string</returns>
    public object GetValue(int i)
    {
        return Values.ElementAt(i)!;
    }

    /// <summary>
    /// Adds values to the vector.
    /// </summary>
    /// <param name="value">T</param>
    public void AddValue(object? value)
    {
        Values.Add(ConvertValue(value));
    }

    /// <summary>
    /// Sets the value at the index.
    /// </summary>
    /// <param name="i">Index</param>
    /// <param name="value">Value</param>
    public void SetValue(int i, object value)
    {
        Values[i] = ConvertValue(value);
    }

    /// <summary>
    /// Removes the value at the index.
    /// </summary>
    /// <param name="i">Index</param>
    public void SetNull(int i)
    {
        Values[i] = default!;
    }

    /// <summary>
    /// Returns the size of the vector.
    /// </summary>
    /// <returns>int</returns>
    public int Size()
    {
        return Values.Count;
    }

    /// <summary>
    /// Converts object into the vector type.
    /// </summary>
    /// <returns>int</returns>
    public T ConvertValue(object? value)
    {
        string input = (string)(value ?? string.Empty);

        return (T)(FieldTypeConverter.ConvertStrValue(GetType(), input) ?? "");
    }
}