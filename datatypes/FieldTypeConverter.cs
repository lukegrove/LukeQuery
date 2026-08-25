using Apache.Arrow.Types;

namespace LukeQuery.DataTypes;

/// <summary>
/// Helper class for converting values to objects.
/// </summary>
public static class FieldTypeConverter
{
    /// <summary>
    /// Converts a string value into an object
    /// </summary>
    /// <param name="type">ArrowType</param>
    /// <param name="value">string?</param>
    /// <returns>object?</returns>
    public static object? ConvertStrValue(ArrowType type, string? value)
    {
        object? result = type switch
        {
            NullType => null,
            BooleanType => bool.Parse(value ?? ""),
            Int8Type => sbyte.Parse(value ?? ""),
            Int16Type => short.Parse(value ?? ""),
            Int32Type => int.Parse(value ?? ""),
            Int64Type => long.Parse(value ?? ""),
            UInt8Type => byte.Parse(value ?? ""),
            UInt16Type => ushort.Parse(value ?? ""),
            UInt32Type => uint.Parse(value ?? ""),
            UInt64Type => ulong.Parse(value ?? ""),
            DoubleType => double.Parse(value ?? ""),
            FloatType => float.Parse(value ?? ""),
            Date32Type => DateTime.Parse(value ?? ""),
            IntervalType => TimeSpan.Parse(value ?? ""),
            _ => value ?? string.Empty,
        };
        return result;
    }
}