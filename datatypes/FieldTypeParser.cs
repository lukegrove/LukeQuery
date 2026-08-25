using System.Globalization;
using Apache.Arrow.Types;

namespace LukeQuery.Datatypes;

/// <summary>
/// Helper class for parsing field types.
/// </summary>
public static class FieldTypeParser
{
    /// <summary>
    /// Can the string value be parsed as a boolean?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseBoolean(string? value)
    {
        return bool.TryParse(value, out _);
    }

    /// <summary>
    /// Can the string value be parsed as a short byte?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseShortByte(string? value)
    {
        return sbyte.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as a short?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseShort(string? value)
    {
        return short.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as an int?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseInt(string? value)
    {
        return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as a long?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseLong(string? value)
    {
        return long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as a byte?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseByte(string? value)
    {
        return byte.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as an unsigned short?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseUShort(string? value)
    {
        return ushort.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as an unsigned int?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseUInt(string? value)
    {
        return uint.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as an unsigned long?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseULong(string? value)
    {
        return ulong.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as a float?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseFloat(string? value)
    {
        return float.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as a double?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseDouble(string? value)
    {
        return double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as a date?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseDate(string? value)
    {
        return DateTime.TryParse(value, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Can the string value be parsed as an interval?
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>Can be parsed?</returns>
    public static bool CanParseInterval(string? value)
    {
        return TimeSpan.TryParse(value, CultureInfo.InvariantCulture, out _);
    }

    /// <summary>
    /// Returns the inferred type of the string value.
    /// </summary>
    /// <param name="value">string?</param>
    /// <returns>ArrowType</returns>
    public static ArrowType InferType(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return ArrowTypes.NullType;
        }

        if (CanParseBoolean(value))
        {
            return ArrowTypes.BooleanType;
        }

        if (CanParseShortByte(value))
        {
            return ArrowTypes.Int8Type;
        }

        if (CanParseShort(value))
        {
            return ArrowTypes.Int16Type;
        }

        if (CanParseInt(value))
        {
            return ArrowTypes.Int32Type;
        }

        if (CanParseLong(value))
        {
            return ArrowTypes.Int64Type;
        }

        if (CanParseByte(value))
        {
            return ArrowTypes.UInt8Type;
        }

        if (CanParseUShort(value))
        {
            return ArrowTypes.UInt16Type;
        }

        if (CanParseUInt(value))
        {
            return ArrowTypes.UInt32Type;
        }

        if (CanParseULong(value))
        {
            return ArrowTypes.UInt64Type;
        }

        if (CanParseDouble(value))
        {
            return ArrowTypes.DoubleType;
        }

        if (CanParseFloat(value))
        {
            return ArrowTypes.FloatType;
        }

        if (CanParseDate(value))
        {
            return ArrowTypes.DateDayType;
        }

        if (CanParseInterval(value))
        {
            return ArrowTypes.IntervalDayTimeType;
        }

        return ArrowTypes.StringType;
    }
}